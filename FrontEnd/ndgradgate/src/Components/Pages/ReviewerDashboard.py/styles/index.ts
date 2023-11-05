import { Button, Form, Table as BSTable } from "react-bootstrap";
import styled from "styled-components";

export const DashboardContainer = styled.div`
  padding: 20px;
  background: #f4f7fa;
  min-height: 100vh;
  font-family: "Arial", sans-serif;
`;
export const Section = styled.section`
  background: white;
  padding: 20px;
  border-radius: 10px;
  margin-bottom: 20px;
  box-shadow: 0 4px 8px rgba(0, 0, 0, 0.05);
`;
export const Header = styled.h2`
  color: #343a40;
  margin-bottom: 25px;
  font-size: 24px;
`;
export const MainContent = styled.div`
  flex-grow: 1;
  padding: 20px;
  background: #f8f9fa;
`;

export const Table = styled(BSTable)`
  th {
    background-color: #5c7cfa;
    color: white;
  }

  thead th {
    border-top: none;
  }

  tbody tr:hover {
    background-color: #f2f2f2;
  }
`;

export const ApplicationRow = styled.div`
  display: flex;
  justify-content: space-between;
  align-items: center;
  padding: 10px;
  border-bottom: 1px solid #eee;

  &:last-child {
    border-bottom: none;
  }

  span {
    font-weight: bold;
  }
`;

export const StyledButton = styled(Button)`
  margin: 0 8px;
  border-radius: 5px;

  &:hover {
    opacity: 0.9;
  }
` as any;

export const ExportButton = styled.button`
  background-color: #007bff;
  color: white;
  padding: 0.375rem 0.75rem;
  border: none;
  border-radius: 0.25rem;
  margin-right: 0.5rem;

  &:hover {
    background-color: #0056b3;
  }
`;
export const AssignmentForm = styled.div`
  display: flex;
  justify-content: space-between;
  align-items: center;
  margin-bottom: 10px;

  span {
    font-weight: bold;
    flex-grow: 1;
    margin-right: 10px;
  }

  Form.Select {
    flex-grow: 2;
    margin-right: 10px;
  }
`;

export const FacultyList = styled.ul`
  list-style-type: none;
  padding: 0;
  margin-top: 10px;

  li {
    padding: 5px 0;
    border-bottom: 1px solid #eee;

    &:last-child {
      border-bottom: none;
    }
  }
`;

export const SearchInput = styled(Form.Control)`
  margin-bottom: 15px;
`;

