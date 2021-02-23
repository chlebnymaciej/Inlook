import { Http2ServerResponse } from "http2"
import getUserToken from "../Authorization/getUserToken"
import { handleError, handleResponse, IApiResponse } from "./apiUtils"
import { BASE_URL } from "./urls"

const targetUrl = BASE_URL + "attachment/"

export const getFile = async (id: string, fileName?: string) => {
    let url = targetUrl + "GetFile?id=" + id;
    type T = IApiResponse<null>;
    return fetch(url, {
        method: "GET",
        headers: new Headers({
            'Accept': '*/*',
            'Authorization': 'Bearer ' + await getUserToken(),
        }),
    })
        .then<T>(async (response) => {
            if (response.ok) {
                response.blob()
                    .then((blob) => {
                        const url = window.URL.createObjectURL(new Blob([blob]));
                        const link = document.createElement('a');
                        link.href = url;
                        link.setAttribute('download', fileName || "attachment.bin");
                        document.body.appendChild(link);
                        link.click();
                        link.parentNode?.removeChild(link);
                    });
                return {
                    isError: false,
                    responseCode: response.status,
                }
            }
            else {
                return {
                    isError: true,
                    responseCode: response.status,
                    errorMessage: await response.text(),
                }
            }

        })
        .catch<T>(handleError);

}

export interface AttachmentInfo {
    id: string;
    clientFileName: string;
    azureFileName: string;
}

export const uploadAttachment = async (file: File) => {
    let url = targetUrl + "UploadAttachment";
    type T = IApiResponse<AttachmentInfo>;
    let formData = new FormData();
    formData.append("file", file);
    return fetch(url, {
        method: "POST",
        headers: new Headers({
            'Accept': 'application/json',
            'Authorization': 'Bearer ' + await getUserToken(),
        }),
        body: formData,
    }).then<T>(handleResponse).catch<T>(handleError);
}

export const deleteAttachment = async (id: string) => {
    let url = targetUrl + "DeleteAttachment?id=" + id;
    type T = IApiResponse<Http2ServerResponse>;
    return fetch(url, {
        method: "DELETE",
        headers: new Headers({
            'Accept': 'application/json',
            'Authorization': 'Bearer ' + await getUserToken(),
            "Content-Type": "application/json"
        }),
    }).then<T>(handleResponse).catch<T>(handleError);
}