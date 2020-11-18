import { Http2ServerResponse } from "http2"
import getUserToken from "../Authorization/getUserToken"
import { handleError, handleResponse, IApiResponse } from "./apiUtils"
import { BASE_URL } from "./urls"

const target_url = BASE_URL + "mail/"

export interface MailModel {
    to: string[];
    cc: string[] | null;
    bcc: string[] | null;
    subject: string | null;
    text: string | null;
}

export const postMail = async (mail: MailModel) => {
    let url = target_url + "sendMail";
    type T = IApiResponse<Http2ServerResponse>;
    return fetch(url, {
        method: "POST",
        headers: new Headers({
            'Accept': 'application/json',
            'Authorization': 'Bearer ' + await getUserToken(),
            "Content-Type": "application/json"
        }),
        body: JSON.stringify(mail)
    }).then<T>(handleResponse).catch<T>(handleError);
}

