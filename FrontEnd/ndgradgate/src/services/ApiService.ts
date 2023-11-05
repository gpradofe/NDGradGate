import axios from "axios";
import { Applicant } from "../types/Application/Applicant";
import { Faculty } from "../types/Application/Faculty";

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
    const mockFaculty: Faculty[] = [
      { id: 1, name: "Dr. Smith", department: "Computer Science" },
      { id: 2, name: "Dr. Johnson", department: "Engineering" },
      // ... more faculty members
    ];
    console.log("Faculty loaded:", mockFaculty);
    return mockFaculty;
  }
}

const apiServiceInstance = new ApiService();

export default apiServiceInstance;
