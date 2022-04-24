<div id="top"></div>

<br />
<div align="center">

<h3 align="center">PositionPortal</h3>

  <p align="center">
    An investment tracker for all the stock and crypto positions you hold.
    <br />
    <a href="https://github.com/abomb07/PositionPortal"><strong>Explore the docs »</strong></a>
    <br />
  </p>
</div>



<!-- TABLE OF CONTENTS -->
<details>
  <summary>Table of Contents</summary>
  <ol>
    <li>
      <a href="#introduction">Introduction</a>
      <ul>
        <li><a href="#built-with">Built With</a></li>
      </ul>
    </li>
    <li><a href="#functionality">Functionality</a></li>
    <li><a href="#new-tech">Technologies Learned</a></li>
    <li><a href="#design-approach">Design</a></li>
    <li><a href="#risks">Risks & Challenges</a></li>
    <li><a href="#issues">Known Issues</a></li>
    <li><a href="#contact">Contact</a></li>
    <li><a href="#acknowledgments">Acknowledgments</a></li>
  </ol>
</details>



<!-- ABOUT THE PROJECT -->
## Introduction

<p>The PositionPortal web application is a stock and cryptocurrency position tracker. Users can enter their current positions in different stocks and coins so they can follow all of their investments on one site. Stock and crypto data such as the current price and 90-day chart are gathered to give the user the history of the position. A history log of all prior investments is kept allowing for insight into what investments have worked and what has not. This site is designed for retail traders who invest in different stocks and coins using different brokerages and wallets. As new coins pop up in the crypto market, it is sometimes hard to find an exchange that allows for the trading of that coin. This means that many different crypto wallets are needed to have a diversified crypto portfolio. The same problem occurs in the use of stock brokerages such as Fidelity, Robinhood, and TD Ameritrade. The purpose of this project is to eliminate these problems by using one site where all investments can be tracked at once.</p>
<img src="https://user-images.githubusercontent.com/57200691/164996454-9cd27459-943c-43a5-8a00-99df3f119f92.png">
<p align="right">(<a href="#top">back to top</a>)</p>



### Built With

* [Vue.js](https://vuejs.org/)
* [Vuex](https://vuex.vuejs.org/)
* [ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/introduction-to-aspnet-core?view=aspnetcore-6.0)
* [Bootstrap](https://getbootstrap.com)
* [JQuery](https://jquery.com)
* [TradingVueJS](https://github.com/tvjsx/trading-vue-js)

<p align="right">(<a href="#top">back to top</a>)</p>



## Functionality

* Overview of all open positions

<img src="https://user-images.githubusercontent.com/57200691/164997003-b7e2e290-8d60-477c-a33e-0b9786cff50f.png">

* Overview of all open stock positions

<img src="https://user-images.githubusercontent.com/57200691/164997043-e0864dc8-ee67-4735-8321-f324c5057696.png">

* Overview of all open crypto positions

<img src="https://user-images.githubusercontent.com/57200691/164997070-0efb5071-508f-4b33-8da5-5c76671dfcff.png">

* Quote, balance, cost basis, gain/loss details for positions

<img src="https://user-images.githubusercontent.com/57200691/164997086-b63baf58-6c80-49e1-a408-f1f5c918b2e5.png">

* 90-day chart of historical data for the position

<img src="https://user-images.githubusercontent.com/57200691/164997102-4f8ea77d-a49b-4327-9b8f-ccf7d90933d9.png">

* History of closed positions

<img src="https://user-images.githubusercontent.com/57200691/164997116-3dd3b017-ea4a-457a-8cb6-fefba8ebbc8c.png">

* Gain/loss of closed positions

<img src="https://user-images.githubusercontent.com/57200691/164997139-119758ce-5763-42cd-830b-bba8242891a7.png">

## New Tech

New technologies that I learned
* JavaScript - Very common language used in the industry for web app design
* Vue.js - Emerging JS framework
* Vuex - Introduces the concept of stores and mutations to vue
* Bearer toaken authentication - Useful for designing a secure API

## Design Approach

* Vue.js uses Fetch API to make get requests to the API service I made in .NET Core

<img src="https://user-images.githubusercontent.com/57200691/164997422-308c963b-3fe7-438b-bf9c-90fc941877a9.png">

* Flowchart

<img src="https://user-images.githubusercontent.com/57200691/164997492-4af3de73-a806-494a-98e2-413ba6ff1763.png">

* UML Class Diagram

<img src="https://user-images.githubusercontent.com/57200691/164997521-e5b37378-769b-4805-9b44-e2c231354ad0.png">

## Risks

I went into this project with zero knowledge of JavaScript or Vue. I knew it would be a huge risk to try to learn and implement these technologies in one semester. But, I wanted to challenge myself so I used the extensive Vue documentation to my advantage. I was able to learn Vue through trial and error as well as JavaScript.

## Issues

TradingVue uses unix timestamps. Since the stock market is closed on weekends, there are some gaps in the chart. The api I used to get the data does not return any unix timestamps for these weekends. Further research is needed to understand how TradingVue handles these days. As for now, it is an issue.
<img src="https://user-images.githubusercontent.com/57200691/164997773-bacccbec-4f26-4593-aee2-31be0528a53a.png">

## Contact

Adam Bender - abend07@gmail.com

[![LinkedIn][linkedin-shield]][linkedin-url]

Project Link: [https://github.com/abomb07/PositionPortal](https://github.com/abomb07/PositionPortal)

<p align="right">(<a href="#top">back to top</a>)</p>


<!-- MARKDOWN LINKS & IMAGES -->
[linkedin-shield]: https://img.shields.io/badge/-LinkedIn-black.svg?style=for-the-badge&logo=linkedin&colorB=555
[linkedin-url]: https://linkedin.com/in/adam-bender-3b545b1a9
