import { Button } from "primereact/button";
import { styled } from "styled-components";

export const ActionButton = styled(Button)`
  margin: 0 5px;
  border-radius: 50%; // Makes the button round
  padding: 0.5rem; // Adjust padding to make sure the button is circular
  width: 2.5rem; // Fixed width
  height: 2.5rem; // Fixed height to ensure a circle
  line-height: 1; // Adjust line height to align icon vertically
  display: flex;
  align-items: center;
  justify-content: center;

  .p-button-label {
    display: none; // Hide the label text
  }

  &.p-button-success,
  &.p-button-danger {
    border: none; // Removes border
  }

  &.p-button-success {
    background-color: #28a745; // Success color, you can choose your own
    &:hover {
      background-color: #218838; // Darken on hover, optional
    }
  }

  &.p-button-danger {
    background-color: #dc3545; // Danger color, you can choose your own
    &:hover {
      background-color: #c82333; // Darken on hover, optional
    }
  }

  &.p-button-outlined {
    background-color: transparent;
    color: inherit;
    box-shadow: none;
  }
`;
export const StyledExportButton = styled(Button)`
  border-radius: 5px; // Slightly rounded corners
  padding: 0.5rem 1rem; // Padding inside the button

  &:hover {
    background-color: #e2e2e2; // Slightly darker on hover
    color: #212529; // Optional: Change color on hover
  }

  .pi {
  }
`;
