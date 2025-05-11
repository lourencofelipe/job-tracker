"use client";
import React, { useState, useEffect } from "react";
import { getJobs, createJob, updateJob, deleteJob } from "@/api/jobApi";
import { formatDate, handleApiError, getInitialJobData } from "@/lib/utils";
import {
  Button,
  Input,
  Select,
  SelectTrigger,
  SelectValue,
  SelectContent,
  SelectItem,
  Table,
  TableHeader,
  TableRow,
  TableCell,
  TableBody,
  Calendar,
  Dialog,
  DialogContent,
  DialogHeader,
  DialogTitle,
  DialogFooter,
  AlertDialog,
  AlertDialogAction,
  AlertDialogContent,
  AlertDialogDescription,
  AlertDialogFooter,
  AlertDialogHeader,
  AlertDialogTitle,
} from "@/components/ui";

const JobTracker = () => {
  const [jobs, setJobs] = useState([]);
  const [currentPage, setCurrentPage] = useState(1);
  const jobsPerPage = 10;

  const [isModalOpen, setIsModalOpen] = useState(false);
  const [currentJob, setCurrentJob] = useState(getInitialJobData());
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
      setError(handleApiError(error, "Failed to fetch jobs."));
    }
  };

  useEffect(() => {
    const { companyName, position, status, dateApplied } = currentJob;
    setIsSubmitDisabled(!companyName || !position || !status || !dateApplied);
  }, [currentJob]);

  const handleAddJob = () => {
    setCurrentJob(getInitialJobData());
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
      setCurrentPage(1);
      setIsModalOpen(false);
    } catch (error) {
      setError(handleApiError(error, "Failed to save the job."));
    }
  };

  const handleDeleteJob = async (id) => {
    try {
      await deleteJob(id);
      setJobs((prevJobs) => prevJobs.filter((job) => job.id !== id));
      setCurrentPage(1);
    } catch (error) {
      setError(handleApiError(error, "Failed to delete the job."));
    }
  };

  const handleDateChange = (date) => {
    if (date) {
      setCurrentJob({
        ...currentJob,
        dateApplied: formatDate(date, "iso"),
      });
      setIsCalendarOpen(false);
    }
  };

  const handleCalendarToggle = () => {
    setIsCalendarOpen((prev) => !prev);
  };

  // Pagination
  const totalJobs = jobs.length;
  // If 0 jobs, shows 1 empty page.
  const totalPages = totalJobs === 0 ? 1 : Math.ceil(totalJobs / jobsPerPage);
  // Gets the index of the first application that needs to be shown in the current page.
  const firstJobIndex = (currentPage - 1) * jobsPerPage;
  // The last application in the current page it is the previous to the next page start.
  const lastJobIndex = firstJobIndex + jobsPerPage;
  // Slice the list of jobs just with the applications that needs to be shown in the page.
  const currentJobs = jobs.slice(firstJobIndex, lastJobIndex);

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
            {currentJobs.map((job) => (
              <TableRow
                key={job.id}
                className="hover:bg-gray-700 transition-colors"
              >
                <TableCell>{job.companyName}</TableCell>
                <TableCell>{job.position}</TableCell>
                <TableCell>{job.status}</TableCell>
                <TableCell>{formatDate(job.dateApplied, "display")}</TableCell>
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

        <div className="flex justify-center mt-4 space-x-2">
          <Button
             onClick={() => setCurrentPage(currentPage - 1)}
            disabled={currentPage === 1}
            className="bg-blue-500 hover:bg-blue-600 text-white"
          >
            Previous
          </Button>
          <span className="text-white px-4 py-2">
            Page {currentPage} of {totalPages}
          </span>
          <Button
            onClick={() => setCurrentPage(currentPage + 1)}
            disabled={currentPage === totalPages}
            className="bg-blue-500 hover:bg-blue-600 text-white"
          >
            Next
          </Button>
        </div>
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
                value={formatDate(currentJob.dateApplied, "display")}
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
            <Button variant="outline" onClick={() => setIsModalOpen(false)}>
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
