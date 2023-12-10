import axios from "axios";
import { Applicant } from "../types/Application/Applicant";
import { Faculty } from "../types/Application/Faculty";
import { Setting } from "../types/Settings/Setting";
class ApiService {
  private baseURL: string = "https://localhost:5009/api/"; // Replace with your actual base URL

  public async fetchApplications(): Promise<Applicant[]> {
    try {
      const response = await axios.get<Applicant[]>(
        `${this.baseURL}Applicant/GetAllApplicants`
      );
      console.log("Applications fetched:", response.data);
      return response.data;
    } catch (error) {
      console.error("Error fetching applications:", error);
      throw error;
    }
  }

  // Simulating fetching faculty since no endpoint was provided
  public async fetchFaculty(): Promise<Faculty[]> {
    // Replace with actual API call if needed

    try {
      const response = await axios.get<Faculty[]>(
        `${this.baseURL}Faculty/GetAllFaculty`
      );
      console.log("Faculty fetched:", response.data);
      return response.data;
    } catch (error) {
      console.error("Error fetching faculty:", error);
      throw error;
    }
  }
  public async fetchSettings(): Promise<Setting[]> {
    try {
      const response = await axios.get<Setting[]>(
        `${this.baseURL}Setting/GetAllSettings`
      );
      console.log("Settings fetched:", response.data);
      return response.data;
    } catch (error) {
      console.error("Error fetching settings:", error);
      throw error;
    }
  }
  public async addOrUpdateSetting(setting: Setting): Promise<Setting> {
    try {
      const response = await axios.post<Setting>(
        `${this.baseURL}Setting/AddOrUpdateSetting`,
        setting
      );
      console.log("Setting added/updated:", response.data);
      return response.data;
    } catch (error) {
      console.error("Error adding/updating setting:", error);
      throw error;
    }
  }
}

const apiServiceInstance = new ApiService();

export default apiServiceInstance;
