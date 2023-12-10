import React, {
  createContext,
  useState,
  useContext,
  useEffect,
  ReactNode,
} from "react";
import { Applicant } from "../types/Application/Applicant";
import { Faculty } from "../types/Application/Faculty";
import apiServiceInstance from "../services/ApiService";
import { Setting } from "../types/Settings/Setting";

interface ApplicationContextProps {
  applications: Applicant[];
  faculty: Faculty[];
  fetchApplications: () => void;
  fetchFaculty: () => void;
  addFaculty: (newFaculty: Faculty) => void;
  updateFaculty: (updatedFaculty: Faculty) => void;
  deleteFaculty: (facultyId: number) => void;
  currentUser: Faculty | null;
  setCurrentUser: (user: Faculty | null) => void;
  settings: Setting[];
  fetchSettings: () => void;
  addOrUpdateSetting: (setting: Setting) => void;
}

interface ApplicationProviderProps {
  children: ReactNode;
}

const ApplicationContext = createContext<ApplicationContextProps | undefined>(
  undefined
);

export const useApplicationContext = () => {
  const context = useContext(ApplicationContext);
  if (context === undefined) {
    throw new Error(
      "useApplicationContext must be used within a ApplicationProvider"
    );
  }
  return context;
};

export const ApplicationProvider: React.FC<ApplicationProviderProps> = ({
  children,
}) => {
  const [applications, setApplications] = useState<Applicant[]>([]);
  const [faculty, setFaculty] = useState<Faculty[]>([]);
  const [currentUser, setCurrentUser] = useState<Faculty | null>(() => {
    const storedUser = localStorage.getItem("currentUser");
    return storedUser ? JSON.parse(storedUser) : null;
  });
  const [settings, setSettings] = useState<Setting[]>([]);

  useEffect(() => {
    localStorage.setItem("currentUser", JSON.stringify(currentUser));
  }, [currentUser]);
  const fetchApplications = async () => {
    try {
      const response = await apiServiceInstance.fetchApplications();
      setApplications(response);
    } catch (error) {
      console.error("Error fetching applications:", error);
    }
  };

  const fetchFaculty = async () => {
    try {
      const response = await apiServiceInstance.fetchFaculty();
      setFaculty(response);
    } catch (error) {
      console.error("Error fetching faculty:", error);
    }
  };
  const addFaculty = (newFaculty: Faculty) => {
    setFaculty([...faculty, newFaculty]);
  };

  const updateFaculty = (updatedFaculty: Faculty) => {
    setFaculty(
      faculty.map((f) => (f.Id === updatedFaculty.Id ? updatedFaculty : f))
    );
  };

  const deleteFaculty = (facultyId: number) => {
    setFaculty(faculty.filter((f) => f.Id !== facultyId));
  };
  const fetchSettings = async () => {
    try {
      const fetchedSettings = await apiServiceInstance.fetchSettings();
      setSettings(fetchedSettings);
    } catch (error) {
      console.error("Error fetching settings:", error);
    }
  };

  const addOrUpdateSetting = async (setting: Setting) => {
    try {
      await apiServiceInstance.addOrUpdateSetting(setting);
      fetchSettings(); // Refetch settings after update
    } catch (error) {
      console.error("Error adding/updating setting:", error);
    }
  };
  useEffect(() => {
    fetchApplications();
    fetchFaculty();
    fetchSettings();
  }, []);

  return (
    <ApplicationContext.Provider
      value={{
        applications,
        faculty,
        currentUser,
        setCurrentUser,
        fetchApplications,
        fetchFaculty,
        addFaculty,
        updateFaculty,
        deleteFaculty,
        settings,
        fetchSettings,
        addOrUpdateSetting,
      }}
    >
      {children}
    </ApplicationContext.Provider>
  );
};
