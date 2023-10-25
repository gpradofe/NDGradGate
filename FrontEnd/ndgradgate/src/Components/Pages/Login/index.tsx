import React from "react";
import { Form, Button } from "react-bootstrap";
import { LoginContainer, LoginForm } from "./styles";

const LoginPage: React.FC = () => {
  return (
    <LoginContainer>
      <LoginForm>
        <h2 className="text-center mb-4">NDGradGate Login</h2>
        <Form.Group className="mb-3" controlId="formBasicEmail">
          <Form.Label>Email address</Form.Label>
          <Form.Control type="email" placeholder="Enter email" />
        </Form.Group>

        <Form.Group className="mb-3" controlId="formBasicPassword">
          <Form.Label>Password</Form.Label>
          <Form.Control type="password" placeholder="Password" />
        </Form.Group>

        <Button variant="primary" type="submit">
          Sign In
        </Button>
      </LoginForm>
    </LoginContainer>
  );
};

export default LoginPage;
