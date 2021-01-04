import { Http2ServerResponse } from "http2"
import getUserToken from "../Authorization/getUserToken"
import { handleError, handleResponse, IApiResponse } from "./apiUtils"
import { AttachmentInfo } from "./attachmentsApi"
import { BASE_URL } from "./urls"
import { UserModel } from "./userApi"

const target_url = BASE_URL + "mail/"

export interface MailModel {
    to: string[];
    cc: string[] | null;
    bcc: string[] | null;
    subject: string | null;
    text: string | null;
    attachments: string[] | null;
}
export type OrderType = 'asc' | 'desc';

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


export interface EmailProps {
    subject: string;
    from: UserModel;
    to: UserModel[];
    cc: UserModel[];
    sendTime: Date;
    text: string;
    read: boolean;
    mailId: string;
    attachments: AttachmentInfo[];
}
export interface EmailPropsPageModel {
    emailsPage: EmailProps[];
    totalCount: number;

}
export const getMails = async () => {
    let url = target_url + "GetMails";
    type T = IApiResponse<EmailProps[]>;
    return fetch(url, {
        method: "GET",
        headers: new Headers({
            'Accept': 'application/json',
            'Authorization': 'Bearer ' + await getUserToken(),
            "Content-Type": "application/json"
        })
    }).then<T>(handleResponse).catch<T>(handleError);
}

export const getEmailList = async (page: number = 0, pageSize: number = 10, searchText?: string, orderBy?: keyof EmailProps, orderType?: OrderType) => {
    type T = IApiResponse<EmailPropsPageModel>;
    let url = target_url + "getContactList";
    url += `?page=${page}`;
    url += `&pageSize=${pageSize}`;
    if (searchText) url += `&searchText=${searchText}`;
    if (orderBy) url += `&orderBy=${orderBy}`;
    if (orderType) url += `&orderType=${orderType}`;
    return fetch(url, {
        method: "GET",
        headers: new Headers({
            'Accept': 'application/json',
            'Authorization': 'Bearer ' + await getUserToken(),
        }),
    }).then<T>(handleResponse).catch<T>(handleError);
}

export const setReadMailStatus = async (mailId: string, read: boolean) => {
    let url = target_url + 'ReadMailStatus';
    type T = IApiResponse<null>;
    return fetch(url, {
        method: "PUT",
        headers: new Headers({
            'Accept': 'application/json',
            'Authorization': 'Bearer ' + await getUserToken(),
            "Content-Type": "application/json"
        }),
        body: JSON.stringify({
            mailId, read
        })
    }).then<T>(handleResponse).catch<T>(handleError);
}

