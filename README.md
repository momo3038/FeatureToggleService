# FeatureToggleService
A simple REST Seft-hosted service to provide toggle feature service — Edit

[![Build status](https://ci.appveyor.com/api/projects/status/0ugc2ogv9rcqfvx2?svg=true)](https://ci.appveyor.com/project/momo3038/featuretoggleservice)

**Todo for Client assembly**
- [x] Remove TimeSpan from WebApi ctr. Add Timespan delay in the configuration file.
- [ ] Add system to poll web api with HEAD request to know if feature toggle should be retrieved.
- [ ] Add cache system in client and handle error when the web api is not available.
- [ ] Handle retry with Polly if Web Api is not available (Stop polling in that case).
- [ ] Add a degradated mode (By conf) to save somewhere in client apps the feature toggle retrieved. (json texte file. Binary ?). The goal is to be totally independant is case of big problem with the feature toggle service.
- [ ] Test Interfaces by Adding another provider (File provider ? Memory provider ?) in client
 
**Todo for Backend assembly**
- [ ] Add system to quickly know if state off the world has changed for all or for a feature type 
- [ ] Test Interfaces by Adding another provider (File provider ? Memory provider ?) in admin.
- [ ] Add a system to quickly add or remove a feature toggle by script (SQL or something else).

**Todo for Admin website**
- [ ] Add a admin UI to handle CRUD operation on feature toggle.
- [ ] Visualize audit log

**Todo for Front Lib**
- [ ] Add a front system to retreive toggle from the Web Api or directly requesting the admin service.

**Todo for Demo assembly**
- [ ] Add a client demo to test the polling system and the full integration mode.
