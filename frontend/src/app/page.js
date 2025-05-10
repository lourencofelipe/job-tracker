"use client";
import React, { useState, useEffect } from "react";
import {
  getJobs,
  createJob,
  updateJob,
  deleteJob,
} from "@/api/jobApi";

import { Button } from "@/components/ui/button";
import { Input } from "@/components/ui/input";
import {
  Select,
  SelectTrigger,
  SelectValue,
  SelectContent,
  SelectItem,
} from "@/components/ui/select";
import {
  Table,
  TableHeader,
  TableRow,
  TableCell,
  TableBody,
} from "@/components/ui/table";
import { Calendar } from "@/components/ui/calendar";
import {
  Dialog,
  DialogContent,
  DialogHeader,
  DialogTitle,
  DialogFooter,
} from "@/components/ui/dialog";
import {
  AlertDialog,
  AlertDialogAction,
  AlertDialogContent,
  AlertDialogDescription,
  AlertDialogFooter,
  AlertDialogHeader,
  AlertDialogTitle,
} from "@/components/ui/alert-dialog";
import { format } from 'date-fns'

const JobTracker = () => {
  const [jobs, setJobs] = useState([]);
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [currentJob, setCurrentJob] = useState({
    companyName: "",
    position: "",
    status: "",
    dateApplied: "",
  });
  const [isSubmitDisabled, setIsSubmitDisabled] = useState(true);
  const [isCalendarOpen, setIsCalendarOpen] = useState(false);
  const [error, setError] = useState(null);

  useEffect(() => {
    fetchJobs();
  }, []);

  const fetchJobs = async () => {
    try {
      const data = await getJobs();
      setJobs(data);
    } catch (error) {
      const errorMessage = error.response?.data?.message || error.message || "Failed to fetch jobs. Please try again later.";
      setError(errorMessage);
    }
  };

  useEffect(() => {
    const { companyName, position, status, dateApplied } = currentJob;
    setIsSubmitDisabled(!companyName || !position || !status || !dateApplied);
  }, [currentJob]);

  const handleAddJob = () => {
    setCurrentJob({
      companyName: "",
      position: "",
      status: "",
      dateApplied: "",
    });
    setIsModalOpen(true);
  };

  const handleEditJob = (job) => {
    setCurrentJob({
      id: job.id,
      companyName: job.companyName,
      position: job.position,
      status: job.status,
      dateApplied: job.dateApplied,
    });
    setIsModalOpen(true);
  };

  const handleSubmit = async () => {
    try {
      if (currentJob.id) {
        await updateJob(currentJob);
        setJobs((prevJobs) =>
          prevJobs.map((j) =>
            j.id === currentJob.id ? { ...currentJob } : j
          )
        );
      } else {
        const newJob = await createJob(currentJob);
        setJobs((prevJobs) => [...prevJobs, newJob]);
      }
      setIsModalOpen(false);
    } catch (error) {
      const errorMessage = error.response?.data?.message || error.message || "Failed to save the job. Please try again.";
      setError(errorMessage);
    }
  };

  const handleDeleteJob = async (id) => {
    try {
      await deleteJob(id);
      setJobs((prevJobs) => prevJobs.filter((job) => job.id !== id));
    } catch (error) {
      const errorMessage = error.response?.data?.message || error.message || "Failed to delete the job. Please try again.";
      setError(errorMessage);
    }
  };

  const handleDateChange = (date) => {
    if (date) {
      const formattedDate = format(date, "yyyy-MM-dd");
      setCurrentJob({
        ...currentJob,
        dateApplied: formattedDate,
      });
      setIsCalendarOpen(false);
    }
  };

  const handleCalendarToggle = () => {
    setIsCalendarOpen((prev) => !prev);
  };

  return (
    <div className="p-6 space-y-6 flex flex-col items-center min-h-screen bg-gray-950 text-white font-sans">
      <div className="bg-gray-900 w-full p-4 flex justify-between items-center shadow-md">
        <h1 className="text-white text-3xl font-bold">Job Tracker</h1>
        <Button
          onClick={handleAddJob}
          className="px-4 py-2 bg-blue-600 text-white rounded-lg shadow-md hover:bg-blue-700 transition-all"
        >
          Add Job
        </Button>
      </div>

      <div className="w-full max-w-4xl bg-gray-800 rounded-lg shadow-lg p-6 mt-8">
        <Table className="w-full">
          <TableHeader>
            <TableRow className="font-bold">
              <TableCell>Company Name</TableCell>
              <TableCell>Position</TableCell>
              <TableCell>Status</TableCell>
              <TableCell>Date Applied</TableCell>
              <TableCell>Actions</TableCell>
              <TableCell>Delete</TableCell>
            </TableRow>
          </TableHeader>
          <TableBody>
            {jobs.map((job) => (
              <TableRow
                key={job.id}
                className="hover:bg-gray-700 transition-colors"
              >
                <TableCell>{job.companyName}</TableCell>
                <TableCell>{job.position}</TableCell>
                <TableCell>{job.status}</TableCell>
                <TableCell>
                  {job.dateApplied
                    ? new Date(job.dateApplied).toLocaleDateString("en-GB")
                    : ""}
                </TableCell>
                <TableCell>
                  <Button
                    variant="secondary"
                    onClick={() => handleEditJob(job)}
                    className="bg-gray-600 hover:bg-gray-700 text-white rounded"
                  >
                    Edit
                  </Button>
                </TableCell>
                <TableCell>
                  <Button
                    variant="secondary"
                    onClick={() => handleDeleteJob(job.id)}
                    className="bg-red-600 hover:bg-red-700 text-white rounded"
                  >
                    Delete
                  </Button>
                </TableCell>
              </TableRow>
            ))}
          </TableBody>
        </Table>
      </div>

      <Dialog open={isModalOpen} onOpenChange={setIsModalOpen}>
        <DialogContent className="bg-gray-800 text-white rounded-lg shadow-lg p-6">
          <DialogHeader>
            <DialogTitle className="text-lg font-semibold">
              {currentJob?.id ? "Edit Job Application" : "Add Job"}
            </DialogTitle>
          </DialogHeader>

          <div className="space-y-4">
            <Input
              name="companyName"
              placeholder="Company Name"
              value={currentJob.companyName}
              onChange={(e) =>
                setCurrentJob({ ...currentJob, companyName: e.target.value })
              }
              className="bg-gray-700 text-white border border-gray-600 rounded"
            />

            <Input
              name="position"
              placeholder="Position"
              value={currentJob.position}
              onChange={(e) =>
                setCurrentJob({ ...currentJob, position: e.target.value })
              }
              className="bg-gray-700 text-white border border-gray-600 rounded"
            />

            <div className="relative">
              <Input
                name="dateApplied"
                placeholder="Select Date"
                value={
                  currentJob.dateApplied
                    ? new Date(currentJob.dateApplied).toLocaleDateString(
                        "en-GB"
                      )
                    : ""
                }
                onClick={handleCalendarToggle}
                readOnly
                className="bg-gray-700 text-white border border-gray-600 rounded"
              />
              {isCalendarOpen && (
                <div className="absolute z-50 bg-gray-800 border border-gray-600 rounded shadow-md p-2">
                  <Calendar
                    mode="single"
                    selected={
                      currentJob.dateApplied
                        ? new Date(currentJob.dateApplied)
                        : undefined
                    }
                    onSelect={handleDateChange}
                  />
                </div>
              )}
            </div>

            <Select
              value={currentJob.status}
              onValueChange={(value) =>
                setCurrentJob({ ...currentJob, status: value })
              }
            >
              <SelectTrigger className="w-full bg-gray-700 text-white border border-gray-600 rounded">
                <SelectValue placeholder="Select Status" />
              </SelectTrigger>
              <SelectContent>
                <SelectItem value="Applied">Applied</SelectItem>
                <SelectItem value="Interview">Interview</SelectItem>
                <SelectItem value="Offer">Offer</SelectItem>
                <SelectItem value="Rejected">Rejected</SelectItem>
              </SelectContent>
            </Select>
          </div>

          <DialogFooter className="pt-4">
            <Button
              onClick={handleSubmit}
              disabled={isSubmitDisabled}
              className="bg-blue-600 hover:bg-blue-700 text-white rounded"
            >
              Submit
            </Button>
            <Button
              variant="outline"
              onClick={() => setIsModalOpen(false)}
            >
              Cancel
            </Button>
          </DialogFooter>
        </DialogContent>
      </Dialog>

      <AlertDialog open={!!error} onOpenChange={() => setError(null)}>
        <AlertDialogContent className="bg-gray-800 text-white">
          <AlertDialogHeader>
            <AlertDialogTitle>Error</AlertDialogTitle>
            <AlertDialogDescription className="text-gray-300">
              {error}
            </AlertDialogDescription>
          </AlertDialogHeader>
          <AlertDialogFooter>
            <AlertDialogAction
              onClick={() => setError(null)}
              className="bg-blue-600 hover:bg-blue-700 text-white"
            >
              OK
            </AlertDialogAction>
          </AlertDialogFooter>
        </AlertDialogContent>
      </AlertDialog>
    </div>
  );
};

export default JobTracker;
