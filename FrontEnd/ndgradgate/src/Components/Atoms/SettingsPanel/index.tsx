import {
  Accordion,
  AccordionDetails,
  AccordionSummary,
  List,
  ListItem,
  Typography,
} from "@mui/material";
import { TabPanel } from "../../../Helpers/TabPanelHelper";
import { FormControl } from "react-bootstrap";
import { StyledButton } from "../../Pages/AdminDashboard/styles";
import { useApplicationContext } from "../../../context/ApplicationContext";
import { useEffect, useState } from "react";
import { Setting } from "../../../types/Settings/Setting";
import { toast } from "react-toastify";
import ExpandMoreIcon from "@mui/icons-material/ExpandMore";

interface SettingsPanelProps {
  valueTabIndex: number;
  index: number;
}
const SettingsPanel: React.FC<SettingsPanelProps> = ({
  valueTabIndex,
  index,
}) => {
  const { settings, fetchSettings, addOrUpdateSetting } =
    useApplicationContext();
  const [editedSettings, setEditedSettings] = useState<{
    [key: number]: string[];
  }>({});
  const [isLoading, setIsLoading] = useState(false);

  const [changedSettingIds, setChangedSettingIds] = useState<Set<number>>(
    new Set()
  );

  const [localSettings, setLocalSettings] = useState(settings);
  const [isSettingsChanged, setIsSettingsChanged] = useState(false);
  const [expandedPanel, setExpandedPanel] = useState<string | false>(false);

  const handlePanelChange =
    (panel: string) => (event: React.SyntheticEvent, isExpanded: boolean) => {
      setExpandedPanel(isExpanded ? panel : false);
    };

  useEffect(() => {
    setLocalSettings(settings);
  }, [settings]);
  const handleAddSetting = (newSetting: Setting) => {
    setLocalSettings([...localSettings, newSetting]);
    setIsSettingsChanged(true);
  };

  const handleUpdateSetting = (updatedSetting: Setting) => {
    setLocalSettings(
      localSettings.map((s) =>
        s.Id === updatedSetting.Id ? updatedSetting : s
      )
    );
    setIsSettingsChanged(true);
  };
  const handleDeleteSettingValue = (settingId: number, valueIndex: number) => {
    // Update localSettings to remove the value at the specified index
    const updatedLocalSettings = localSettings.map((setting) => {
      if (setting.Id === settingId) {
        const updatedValues = [...setting.Values];
        updatedValues.splice(valueIndex, 1); // Remove the value at the index
        return { ...setting, Values: updatedValues };
      }
      return setting;
    });

    setLocalSettings(updatedLocalSettings);
    setChangedSettingIds(new Set(changedSettingIds.add(settingId)));
  };
  const handleSettingValueChange = (
    settingId: number,
    newValue: string,
    valueIndex: number
  ) => {
    // Find the setting and update its values
    const updatedLocalSettings = localSettings.map((s) => {
      if (s.Id === settingId) {
        const updatedValues = [...s.Values];
        updatedValues[valueIndex] = newValue;
        return { ...s, Values: updatedValues };
      }
      return s;
    });

    setLocalSettings(updatedLocalSettings);

    // Mark this setting ID as changed
    setChangedSettingIds(new Set(changedSettingIds.add(settingId)));
  };
  const applySettingChanges = async (
    settingId: number,
    event: React.SyntheticEvent
  ) => {
    event.preventDefault();
    setIsLoading(true);
    const settingToUpdate = localSettings.find((s) => s.Id === settingId);
    if (settingToUpdate) {
      settingToUpdate.Values =
        editedSettings[settingId] || settingToUpdate.Values;

      try {
        await addOrUpdateSetting(settingToUpdate);
        await fetchSettings();
        toast.success(
          `Setting "${settingToUpdate.SettingKey}" updated successfully.`
        );
        setEditedSettings({});
        setChangedSettingIds(new Set());
      } catch (error) {
        console.error("Error applying setting changes:", error);
        toast.error("Error updating setting.");
      }
    }
    setIsLoading(false);
  };

  const handleAddNewSettingValue = (settingId: number) => {
    const updatedLocalSettings = localSettings.map((s) => {
      if (s.Id === settingId) {
        return { ...s, Values: [...s.Values, ""] };
      }
      return s;
    });

    setLocalSettings(updatedLocalSettings);
    // Mark this setting ID as changed
    setChangedSettingIds(new Set(changedSettingIds.add(settingId)));
  };
  const revertSettingChanges = (
    settingId: number,
    event: React.SyntheticEvent
  ) => {
    event.preventDefault();

    setLocalSettings(settings);
    setEditedSettings({});
    const newChangedSettingIds = new Set(changedSettingIds);
    newChangedSettingIds.delete(settingId);
    setChangedSettingIds(newChangedSettingIds);
    toast.info("Changes reverted.");
  };
  return (
    <TabPanel value={valueTabIndex} index={1}>
      <h3>Settings</h3>
      {localSettings.map((setting, index) => (
        <Accordion
          key={setting.Id}
          expanded={expandedPanel === `panel${index}`}
          onChange={handlePanelChange(`panel${index}`)}
        >
          <AccordionSummary
            expandIcon={<ExpandMoreIcon />}
            aria-controls={`panel${index}bh-content`}
            id={`panel${index}bh-header`}
          >
            <Typography>{setting.SettingKey}</Typography>
          </AccordionSummary>
          <AccordionDetails>
            <List>
              {setting.Values.map((value, valueIndex) => (
                <ListItem key={valueIndex}>
                  <FormControl
                    as="textarea"
                    value={editedSettings[setting.Id]?.[valueIndex] || value}
                    onChange={(e) =>
                      handleSettingValueChange(
                        setting.Id,
                        e.target.value,
                        valueIndex
                      )
                    }
                  />
                  <StyledButton
                    variant="danger"
                    onClick={() =>
                      handleDeleteSettingValue(setting.Id, valueIndex)
                    }
                  >
                    Delete
                  </StyledButton>
                </ListItem>
              ))}
              <ListItem>
                <StyledButton
                  variant="primary"
                  onClick={() => handleAddNewSettingValue(setting.Id)}
                >
                  Add New Value
                </StyledButton>
              </ListItem>
            </List>
            {changedSettingIds.has(setting.Id) && (
              <>
                <StyledButton
                  variant="secondary"
                  onClick={(e: any) => revertSettingChanges(setting.Id, e)}
                >
                  Revert Changes
                </StyledButton>
                <StyledButton
                  variant="success"
                  onClick={(e: any) => applySettingChanges(setting.Id, e)}
                >
                  Apply Changes
                </StyledButton>
              </>
            )}
          </AccordionDetails>
        </Accordion>
      ))}
    </TabPanel>
  );
};

export default SettingsPanel;
