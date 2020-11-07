import { Http2ServerResponse } from "http2"
import getUserToken from "../Authorization/getUserToken"
import { handleError, IApiResponse } from "./apiUtils"
import { BASE_URL } from "./urls"
import { UserModel } from "./userApi"

const url = BASE_URL + "mail/"

export interface MailModel {
    to: UserModel[];
    cc:UserModel[]  | null;
    subject:string | null;
    text:string | null;
}

export const postMail = async (mail: MailModel) => {
    
    type T = IApiResponse<Http2ServerResponse>;
    return fetch(url ,{
        method: "POST",
        headers: new Headers({
            'Accept': 'application/json',
            'Authorization': 'Bearer ' + await getUserToken(),
            "Content-Type": "application/json"
        }),
        body: JSON.stringify(mail)        
    }).catch<T>(handleError);
}

 