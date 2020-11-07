import { Http2ServerResponse } from "http2"
import getUserToken from "../Authorization/getUserToken"
import { handleError, handleResponse, IApiResponse } from "./apiUtils"
import { BASE_URL } from "./urls"
import { UserModel } from "./userApi"

const groupUrl = BASE_URL + "group/"


export interface GroupModel {
    name: string,
    users: UserModel[] | null,
}

export const getGroups = async () => {
    const url = groupUrl + 'getmygroups'
    type T = IApiResponse<GroupModel[]>;
    return fetch(url ,{
        method: "GET",
        headers: new Headers({
            'Accept': 'application/json',
            'Authorization': 'Bearer ' + await getUserToken(),
        }),
    }).then<T>(handleResponse).catch<T>(handleError);
}

export const postGroup  = async (group: GroupModel) => {
    type T = IApiResponse<Http2ServerResponse>;
    return fetch(groupUrl ,{
        method: "POST",
        headers: new Headers({
            'Accept': 'application/json',
            'Authorization': 'Bearer ' + await getUserToken(),
            "Content-Type": "application/json"
        }),
        body: JSON.stringify(group)
    }).then<T>(handleResponse).catch<T>(handleError);
}

 