import * as OIDC from "oidc-client";


const config: OIDC.UserManagerSettings = {
    authority: "https://login.microsoftonline.com/4a4f96a2-c3a1-4509-9e0e-d3af301e0196/",
    client_id: "48fe1a17-69e2-4942-ab1d-4e288783860f",
    redirect_uri: `${window.location.protocol}//${window.location.host}/callback`,
    post_logout_redirect_uri: `${window.location.protocol}//${window.location.host}/callback`,
    silent_redirect_uri: `${window.location.protocol}//${window.location.host}/callback`,
    automaticSilentRenew: true,
};

const userManager = new OIDC.UserManager(config);

export default userManager;