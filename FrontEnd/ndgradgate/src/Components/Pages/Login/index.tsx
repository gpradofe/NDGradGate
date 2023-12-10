import React, { useState, useContext } from "react";
import { Form, Button, Modal } from "react-bootstrap";
import { useNavigate } from "react-router-dom";
import { LoginContainer, StyledForm } from "./styles";
import { useApplicationContext } from "../../../context/ApplicationContext";

const LoginPage: React.FC = () => {
  const [selectedUserId, setSelectedUserId] = useState<number | null>(null);
  const [password, setPassword] = useState<string>("");
  const [showPasswordModal, setShowPasswordModal] = useState(false);
  const navigate = useNavigate();
  const { faculty, setCurrentUser } = useApplicationContext();

  const handleUserChange = (e: React.ChangeEvent<any>) => {
    setSelectedUserId(parseInt(e.target.value, 10));
  };

  const handlePasswordChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setPassword(e.target.value);
  };

  const handleSubmit = (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    const user = faculty.find((f) => f.Id === selectedUserId);

    if (user) {
      if (user.IsAdmin) {
        setShowPasswordModal(true);
      } else {
        setCurrentUser(user);
        navigate(user.IsReviewer ? "/reviewerDashboard" : "/facultyDashboard");
      }
    } else {
      console.log("Please select a user.");
    }
  };

  const handleAdminLogin = () => {
    if (password === "adminPassword") {
      const user = faculty.find((f) => f.Id === selectedUserId);
      if (user) {
        setCurrentUser(user);
        navigate("/adminDashboard");
      }
    } else {
      console.log("Incorrect password.");
    }
    setShowPasswordModal(false);
  };

  return (
    <LoginContainer>
      <StyledForm onSubmit={handleSubmit}>
        <Form.Group className="mb-3" controlId="formBasicSelect">
          <Form.Label>Select User</Form.Label>
          <Form.Select value={selectedUserId || ""} onChange={handleUserChange}>
            <option value="">Select a user...</option>
            {faculty.map((f) => (
              <option key={f.Id} value={f.Id}>
                {f.Name}
              </option>
            ))}
          </Form.Select>
        </Form.Group>
        <Button variant="primary" type="submit">
          Sign In
        </Button>
      </StyledForm>

      <Modal
        show={showPasswordModal}
        onHide={() => setShowPasswordModal(false)}
      >
        <Modal.Header closeButton>
          <Modal.Title>Admin Login</Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <Form.Group controlId="formPassword">
            <Form.Label>Password</Form.Label>
            <Form.Control
              type="password"
              placeholder="Password"
              value={password}
              onChange={handlePasswordChange}
            />
          </Form.Group>
        </Modal.Body>
        <Modal.Footer>
          <Button
            variant="secondary"
            onClick={() => setShowPasswordModal(false)}
          >
            Close
          </Button>
          <Button variant="primary" onClick={handleAdminLogin}>
            Login
          </Button>
        </Modal.Footer>
      </Modal>
    </LoginContainer>
  );
};

export default LoginPage;
