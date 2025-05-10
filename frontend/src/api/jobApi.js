import axios from "axios";

const API_URL = process.env.NEXT_PUBLIC_API_URL;

export const getJobs = async () => {
  const response = await axios.get(API_URL);
  return response.data;
};

export const createJob = async (job) => {
  const response = await axios.post(API_URL, job);
  return response.data;
};

export const updateJob = async (job) => {
  await axios.put(`${API_URL}/${job.id}`, job);
};

export const deleteJob = async (id) => {
  await axios.delete(`${API_URL}/${id}`);
};
