import React, { useState } from "react";
import { Form, Button } from "react-bootstrap";
import { useNavigate } from "react-router-dom";
import { LoginContainer, StyledForm } from "./styles";
import logo from "./notreDameDrawinig.png";

const LoginPage: React.FC = () => {
  const [email, setEmail] = useState<string>("");
  const [password, setPassword] = useState<string>("");
  const [popup, setPopup] = useState<Window | null>(null);
  const navigate = useNavigate();

  const handleEmailChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setEmail(e.target.value);
  };

  const handlePasswordChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setPassword(e.target.value);
  }
  /*
  const handleSSOLogin = () => {
    const ssoUrl =
      "https://okta.nd.edu/app/zoomus/exk114raeoSv6qgBc357/sso/saml?SAMLRequest=fZJba8IwFMff9ylK3ntJvC7Yis7JBIfF1g32MmJ71GKb1Jy0jH361RtzDHw8kP%2FvXH4ZDL%2BK3KpBY6akT6jjEQtkotJMbn2yiqd2nwyDhwGKImclH1VmJ5dwqACNNUIEbZrck5JYFaAj0HWWwGo598nOmBK560plNKSiAOdbqcKp0D2i3ChaEGvSUDIpzKn1NaD2RjgydSCtXFGW7jHWpOBrT2lbC1BR3T1sx0mr03MR1QlHrKnSCZym88lG5AjEmk188v48%2FvSSzSbtU9FiKfUopd3Hbq8DgvXa7XWPUmEneQbSNAEMBWJWwy8CsYKZRCOk8QnzWMum1PZoTDucMU49p9%2F3PogVamVUovJxJs%2BHq7TkSmCGXDarIzcJj0avc84cj6%2FPj5C%2FxHFoh4soJtbbVQA7CmiUSOTnk99nlZfGJDgb4qeJ9S3hPkBcHZLgn6mBe8sMLuXfTxD8AA%3D%3D&_x_zm_rtaid=OcYLC3CMSgCzEbtmW4U6AQ.1698852130873"; // Your SSO URL here. // Replace with the actual URL
    const newPopup = window.open(ssoUrl, "_blank", "width=500,height=600");
    setPopup(newPopup);

    if (!newPopup) {
      console.error(
        "Failed to open the SSO window. Possibly blocked by a popup blocker."
      );
      return;
    }
  };

  const checkPopup = () => {
    const check = setInterval(() => {
      if (!popup || popup.closed) {
        clearInterval(check);
        console.log("Popup closed by user");
        // Here, ideally, check if the authentication was successful
        // For now, just sending the message
        window.postMessage("SSO_SUCCESS", window.location.origin);
      }
    }, 500);
  };

  checkPopup();
  */
  const handleSubmit = (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    console.log("Form submitted with email:", email);

    // Redirect to /admin_dashboard if email is valid
    if (email) {
      navigate("/adminDashboard"); // Redirect to the dashboard
    } else {
      console.log("Please enter a valid email.");
    }
  };

  return (
    <LoginContainer>
      <StyledForm onSubmit={handleSubmit}>
        <div style={{display: "flex", justifyContent: "center", alignItems: "center", flexDirection: "column"}}>
        <img src={logo} alt="Logo" style={{width: "50%"}}/>
        </div>
        <Form.Group className="mb-3" controlId="formBasicEmail">
          <Form.Label>Email address</Form.Label>
          <Form.Control
            type="email"
            placeholder="Enter email"
            value={email}
            onChange={handleEmailChange}
          />
          <br />
          <Form.Label>Password</Form.Label>
          <Form.Control
            type="password"
            placeholder="Enter password"
            value={password}
            onChange={handlePasswordChange}
          />
        </Form.Group>
        <Button variant="primary" type="submit">
          Sign In
        </Button>
      </StyledForm>
    </LoginContainer>
  );
};

export default LoginPage;
