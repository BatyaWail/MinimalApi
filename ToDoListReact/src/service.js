
import axios from 'axios';

const apiUrl = "http://localhost:5210/items";

export default {
  getTasks: async () => {
    try {
      const result = await axios.get(apiUrl);
      return result.data;
    } catch (error) {
      console.error('Error fetching tasks:', error);
      throw error;
    }
  },

  addTask: async (name) => {
    try {
      const result = await axios.post(apiUrl, { name });
      return result.data;
    } catch (error) {
      console.error('Error adding task:', error);
      throw error;
    }
  },

  setCompleted: async (id, isComplete) => {
    try {
      const result = await axios.put(`${apiUrl}/${id}`, { isComplete });
      return result.data;
    } catch (error) {
      console.error('Error setting task completion:', error);
      throw error;
    }
  },

  deleteTask: async (id) => {
    try {
      await axios.delete(`${apiUrl}/${id}`);
    } catch (error) {
      console.error('Error deleting task:', error);
      throw error;
    }
  }
};
