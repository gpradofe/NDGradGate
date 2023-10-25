import styled from "styled-components";

export const LoginContainer = styled.div`
  display: flex;
  justify-content: center;
  align-items: center;
  height: 100vh;
  background-color: #f2f2f2; // Or any color of your choice
`;

export const LoginForm = styled.form`
  padding: 2rem;
  background: white;
  border-radius: 8px;
  box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
  width: 100%;
  max-width: 400px;

  .form-group {
    margin-bottom: 1rem;
  }

  .btn-primary {
    width: 100%;
  }
`;
