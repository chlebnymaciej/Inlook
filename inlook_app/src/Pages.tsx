import { User } from "oidc-client";
import React, { useEffect, useState } from "react";
import {
  BrowserRouter,
  Switch,
  Route,
} from "react-router-dom";
import userManager from "./Authorization/userManager";
import Topbar from "./Layout/topbar";
import CallbackPage from "./Pages/callbackPage";
import Home from "./Pages/home";

const Pages = () =>  {
  const [user,setUser] = useState<User | null>(null)

  useEffect(()=>{
    (async () => {
      setUser(await userManager.getUser())
    })();
  },[])

    return (<>
      <BrowserRouter>
        <Topbar user={user} />
        <Switch>
        <Route path="/callback">
            <CallbackPage setUser={setUser}/>
        </Route>
        <Route path="/">
            <Home user={user}/>
        </Route>
      </Switch>
    </BrowserRouter>
    </>
    );
  };
  export default Pages;