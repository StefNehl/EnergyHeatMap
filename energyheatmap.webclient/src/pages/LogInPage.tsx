import React, { useState } from "react";
import { Button } from "react-bootstrap";

import "./LoginPage.css"

interface Props {
  isBusy: boolean;
  logIn(userName: string, password: string): Promise<boolean>;
}

const LogInPage: React.FC<Props> = ({ logIn }) => {

  // state
  const [username, setUsername] = useState<string>("");
  const [password, setPassword] = useState<string>("");
  const [isBusy, setIsBusy] = useState<boolean>(false);

  // methods
  const logInClicked = async (e: any) => {
    if (isBusy)
      return;
    e.preventDefault();
    if (username.length <= 0 || password.length <= 0) {
      alert("Benutzername oder Passwort leer!");
      return;
    }
    setIsBusy(true);
    var result = await logIn(username, password);
    if (!result) {
      setIsBusy(false);
      setPassword("");
    }
  };

  // render
  return (
    <div className="formDiv">
      <form onSubmit={logInClicked} className="logInForm">
        <h3>Energy Heat Map</h3>

        <div className="form-group">
          <label>Username</label>
          <input
            className="form-control"
            placeholder="Username"
            value={username}
            onChange={(e) => setUsername(e.target.value)}
          />
        </div>

        <div className="form-group">
          <label>Password</label>
          <input
            type="password"
            className="form-control"
            placeholder="Password"
            value={password}
            onChange={(e) => setPassword(e.target.value)}
          />
        </div>
        <div className="auth-ButtonDiv">
          <Button className="auth-Button" type="submit" variant="dark" disabled={isBusy}>
            Log In
          </Button>
        </div>
      </form>
    </div>

  );
};

export default LogInPage;