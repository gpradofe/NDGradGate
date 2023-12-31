import axios from "axios";
import { Applicant } from "../types/Application/Applicant";
import { Faculty } from "../types/Application/Faculty";
import { Setting } from "../types/Settings/Setting";
class ApiService {
  private baseURL: string = "https://api.gradgate.org/api/"; // Replace with your actual base URL

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
      const payload = {
        Id: setting.Id,
        SettingKey: setting.SettingKey,
        Values: setting.Values,
      };
      console.log("Payload:", payload);
      const response = await axios.post<Setting>(
        `${this.baseURL}Setting/AddOrUpdateSetting`,
        JSON.stringify(payload),
        {
          headers: {
            "Content-Type": "application/json",
          },
        }
      );
      console.log("Setting added/updated:", response.data);
      return response.data;
    } catch (error) {
      console.error("Error adding/updating setting:", error);
      throw error;
    }
  }

  public async updateApplicantStatusAndReviewer(
    updateData: Array<{ Ref: number; FacultyId: Array<number>; Status: string }>
  ): Promise<void> {
    try {
      const response = await axios.put(
        `${this.baseURL}Applicant/UpdateApplicantStatusAndReviewer`,
        JSON.stringify(updateData),
        {
          headers: {
            "Content-Type": "application/json",
          },
        }
      );
      console.log("Applicant status and reviewer updated:", response.data);
    } catch (error) {
      console.error("Error updating applicant status and reviewer:", error);
      throw error;
    }
  }
  public async createApplicants(applicants: Applicant[]): Promise<Applicant[]> {
    try {
      const response = await axios.post<Applicant[]>(
        `${this.baseURL}Applicant/CreateApplicant`,
        JSON.stringify(applicants),
        {
          headers: {
            "Content-Type": "application/json",
          },
        }
      );
      console.log("Applicants created:", response.data);
      return response.data;
    } catch (error) {
      console.error("Error creating applicants:", error);
      throw error;
    }
  }
  public async assignPotentialAdvisorsAndAddComments(
    data: Array<{
      SenderId: number;
      ApplicantID: number;
      PotentialAdvisorId: Array<number>;
      Comment: string;
    }>
  ): Promise<boolean> {
    try {
      const response = await axios.put(
        `${this.baseURL}Faculty/AssignPotentialAdvisorsAndAddComments`,
        JSON.stringify(data),
        {
          headers: {
            "Content-Type": "application/json",
          },
        }
      );
      console.log(
        "Potential advisors assigned and comments added:",
        response.data
      );
      return response.status === 200;
    } catch (error) {
      console.error(
        "Error assigning potential advisors and adding comments:",
        error
      );
      throw error;
    }
  }
  public async saveOrUpdateFaculty(
    facultyData: Array<{
      Id: number;
      Name: string;
      Email: string;
      IsReviewer: boolean;
      Field: string;
    }>
  ): Promise<boolean> {
    try {
      const response = await axios.post(
        `${this.baseURL}Faculty/SaveOrUpdateFaculty`,
        JSON.stringify(facultyData),
        {
          headers: {
            "Content-Type": "application/json",
          },
        }
      );
      console.log("Faculty saved/updated:", response.data);
      return response.status === 200;
    } catch (error) {
      console.error("Error saving/updating faculty:", error);
      throw error;
    }
  }
  public async getAssignedReviewer(reviewerId: number): Promise<number[]> {
    try {
      const response = await axios.get<number[]>(
        `${this.baseURL}Faculty/GetAssignedReviewer?reviewerId=${reviewerId}`,
        {
          headers: {
            accept: "application/json",
          },
        }
      );
      console.log("Assigned reviewer data fetched:", response.data);
      return response.data;
    } catch (error) {
      console.error("Error fetching assigned reviewer data:", error);
      throw error;
    }
  }
}

const apiServiceInstance = new ApiService();

export default apiServiceInstance;
