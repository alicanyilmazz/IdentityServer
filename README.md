# IdentityServer4 Sample Repository

This repository is prepared to exemplify how to use IdentityServer4 and its various flow types. It contains 6 branches, each focusing on different aspects.

## Overview of Flows

- **Client Credential Grant**
- **Authorization Code Grant**
- **Hybrid Grant**
- **Implicit Grant**
- **Resource Owner Credential Grant**

## Topics Covered

When providing examples of these flows, we delve into:

- Storing `ApiScope`, `ApiResource`, `IdentityResources`, and `Client` information in memory.
- Utilizing EntityFramework to persist `ApiScope`, `ApiResource`, `IdentityResources`, and `Client` information in a database.
- Integrating IdentityServer4 with a custom-built membership system.
- Integrating IdentityServer4 with ASP.NET Core Identity membership system.
- Configuring a client application for our SPA Angular application, including the setup of a client and PKCE settings in the `SinglePageApplication` branch.
- Utilizing various endpoints such as `Authorize`, `Token`, `Introspection`, and `Discovery` endpoints using IdentityModel.
- Demonstrating both Cookie and Token-based Authorization.
- Implementing Claim-based Authorization, Policy-based Authorization, and Role-based Authorization with code examples.

## Project Structure

The repository includes two API projects, four client applications, an Authorization Server, and an ASP.NET Core Identity Authorization application, with the structure varying across branches.

Feel free to explore the branches to learn more about IdentityServer4 and its usage in various scenarios.


https://github.com/alicanyilmazz/IdentityServer/assets/49749125/0e37c05e-c2a5-40b1-8c49-35e1f086de86

https://github.com/alicanyilmazz/IdentityServer/assets/49749125/9256ade5-9719-481c-9a56-1a328398acc1

https://github.com/alicanyilmazz/IdentityServer/assets/49749125/ff35bca6-28b9-40b1-a078-8bc0cdc5391c

https://github.com/alicanyilmazz/IdentityServer/assets/49749125/4ffb2574-3eb0-46fa-a5d8-207b92e63ae3

https://github.com/alicanyilmazz/IdentityServer/assets/49749125/af608f51-5026-467a-a802-ffd3dae411b8

https://github.com/alicanyilmazz/IdentityServer/assets/49749125/cf88e01b-4b76-4d82-85eb-6468bf391905

![AspNetCoreIdentity](https://github.com/alicanyilmazz/IdentityServer/assets/49749125/fcd55edb-efd5-42f3-8978-42a7ea1983cd)
