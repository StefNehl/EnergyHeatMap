import React, { useState } from "react";
import { Button } from "react-bootstrap";

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
      <form onSubmit={logInClicked}>
        <h3>Energy Heat Map</h3>

        <div className="form-group">
          <label>Benutzername</label>
          <input
            className="form-control"
            placeholder="Benutzername"
            value={username}
            onChange={(e) => setUsername(e.target.value)}
          />
        </div>

        <div className="form-group">
          <label>Passwort</label>
          <input
            type="password"
            className="form-control"
            placeholder="Passwort"
            value={password}
            onChange={(e) => setPassword(e.target.value)}
          />
        </div>
        <div className={"auth-ButtonDiv"}>
                <Button className={"auth-Button"} type="submit" disabled={isBusy}>
            Anmelden
          </Button>
        </div>
      </form>
    );
};

export default LogInPage;