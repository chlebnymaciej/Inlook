import { User } from "oidc-client";
import userManager from "./userManager";

const getUserToken  = async ()  => {
    let user : User | null = await userManager.getUser();
    if(user === null){
        await userManager.signinRedirect();
        user = await userManager.getUser();
    }
    if(user === null){
       throw new Error("Error while accesing token");
    }
    const expire = user.expires_at;
    if(expire*1000 <= new Date().getTime()){
        user = await userManager.signinSilent();
    }
    
    return user.id_token;
}

export default getUserToken;