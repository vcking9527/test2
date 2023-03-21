ELK Demo

[基本架構]
- 基礎設施: Elasticsearch + Kibana
- 前端架構: Vue2+ Typescript + UI Framework(Vuetify)
- 後端架構: ASP.NET Core 6 Web Api + Swagger Code Gen Api

[Demo流程步驟]
建置基礎設施
1. cd ./script/docker
2. docker-compose up -d
3. 打開kibana的Dev Tool， 將script/accounts.js內的內容進行複製
PUT /accounts/_bulk
{json內容}

啟動前端專案
1. cd/src/client-app
2. npm run serve

啟動後端專案
1. cd/src/ELK.Demo.WebApi
2. dotnet watch run


特色的話，後端主要是採取Swagger Code Generate的方式，會動態去掃描目前專案內的Controller API，然後把它製作成swagger.json，之後產生前端的程式碼，提供給前端專案使用，前端專案不需要手動刻DTO，簡化開發api的流程時間


前端畫面採用Vuetify Material Design，新增Datatable，支援多筆(5、10、15)查詢，也可支援基本的換頁，若未來有時間或時間許可，會考慮再加上filter功能和sorting功能等排列方式，方便查詢

前端部分 因為採用Vue.js，因此可以支援跨平台運行，只需要搭配Node.js即可

後端部分，主要採用主流的ASP.NET Core 6，也是支援跨平台，可以很好的在K8s環境中運行


部署方式，可以採用兩種版本，
第一種是整合部署: 主要是將client-app的原始碼靜態輸出成網頁，放到ASP.NET Core 6專案中，變成一體式的架構，優點是只需要一個image就可以，方便又快速，缺點是當遇到大流量的時候，效率很差

第二種是分離式部署，主要將client-app和ASP.NET Core6的專案各自獨立部署，優點是由於專案是完全分開的，對應流量可以有很好的處理，缺點是通常這種是應付大流量網站或更大型的專案才會考慮
