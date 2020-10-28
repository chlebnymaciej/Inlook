import { User } from "oidc-client";
import userManager from "./userManager";

const getUserToken  = async ()  => {
    let user : User | null = await userManager.getUser();
    if(user === null){
        userManager.signinRedirect();
    }
    else{
        const expire = user.expires_at;
        if(expire*1000 <= new Date().getTime()){
            userManager.signinRedirect();
        }
        
        return user.id_token;
    }
   
}

export default getUserToken;