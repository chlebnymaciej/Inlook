import { Http2ServerResponse } from "http2"
import getUserToken from "../Authorization/getUserToken"
import { handleError, handleResponse, IApiResponse } from "./apiUtils"
import { BASE_URL } from "./urls"
import { UserModel } from "./userApi"

const targetUrl = BASE_URL + "group/"


export interface GroupModel {
    name: string,
    users: UserModel[] | null,
}

export const getGroups = async () => {
    let url = targetUrl+"getMyGroups";
    type T = IApiResponse<GroupModel[]>;
    return fetch(url ,{
        method: "GET",
        headers: new Headers({
            'Accept': 'application/json',
            'Authorization': 'Bearer ' + await getUserToken(),
        }),
    }).then<T>(handleResponse).catch<T>(handleError);
}
export interface CreateGroupModel{
    name: string,
    users: string[]
}

export const postGroup  = async (group: CreateGroupModel) => {
    let url = targetUrl+"postGroup";
    type T = IApiResponse<Http2ServerResponse>;
    return fetch(url ,{
        method: "POST",
        headers: new Headers({
            'Accept': 'application/json',
            'Authorization': 'Bearer ' + await getUserToken(),
            "Content-Type": "application/json"
        }),
        body: JSON.stringify(group)
    }).then<T>(handleResponse).catch<T>(handleError);
}

 