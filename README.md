
<a  name="readme-top"></a>

  

<!-- PROJECT SHIELDS -->

<!--

*** I'm using markdown "reference style" links for readability.

*** Reference links are enclosed in brackets [ ] instead of parentheses ( ).

*** See the bottom of this document for the declaration of the reference variables

*** for contributors-url, forks-url, etc. This is an optional, concise syntax you may use.

*** https://www.markdownguide.org/basic-syntax/#reference-style-links

-->

  
  

![Stable-Release][Stable-Release] ![Forks][Forks] ![Contribution][Contribution] ![Check][Check] ![License][License] ![Downloads][Downloads] ![Commits][Commits] 

  

<!-- PROJECT LOGO -->
![enter image description here](http://vouchcast.com/wp-content/uploads/2024/09/text790.png)
  

<h1  align="center">Vouchcast Business Software</h1>

<p  align="center">

Open Source Self-Hosted ERP/Accounting Software based on ASP.NET (MVC5) web technology

<br  />

<a  href="https://app.vouchcast.com"><strong>Access Demo »</strong></a>

<a  href="https://github.com/Lazarusvc/vcas_business/issues">Report Bug</a>

<a  href="https://github.com/Lazarusvc/vcas_business/issues">Request Feature</a>

</p>

</div>

  
  
  

<!-- TABLE OF CONTENTS -->

<details>

<summary>Table of Contents</summary>

<ol>

<li>

<a  href="#about-the-project">About The Project</a>

<li><a  href="#built-with">Built With</a></li>

</li>

<li><a  href="#getting-started">Getting Started</a></li>

<li><a  href="#prerequisites">Prerequisites</a></li>

<li><a  href="#installation">Installation</a></li>

</ul>

</li>

<li><a  href="#troubleshooting">Troubleshooting</a></li>

<li><a  href="#roadmap">Roadmap</a></li>

<li><a  href="#contributing">Contributing</a></li>

<li><a  href="#license">License</a></li>

<li><a  href="#contact">Contact</a></li>

</ol>

</details>

  
  
  

<!-- ABOUT THE PROJECT -->

## About The Project
![enter image description here](https://vouchcast.com/wp-content/uploads/2025/01/1.png)
  ![enter image description here](http://vouchcast.com/wp-content/uploads/2025/01/5.png)

The Vouchcast Business software is a Progressive Web Application built on Microsoft ASP.NET MVC5 Web Application Technology that allow SMEs to manage their Business through a streamlined, simple and clean interface. The app can be hosted and accessed on a PC, Mobile (Android / iOS), Server or any Windows based Computer with the necessary configurations.

  

The main features are:

- Receivables (Receipt Capture) - POS

- Payables (Issue Payments)

- Charts of Accounts (Debit Account Management)

- Custom Reporting (Sales Report, Profit & Loss Statement)

- Inventory Management

- Customer Management

- Ordering

- Form Management

-  +more

  

<p  align="right">(<a  href="#readme-top">back to top</a>)</p>

### Built With

  

Vouchcast Business is developed with these dependencies:

[![JQuery][JQuery.com]][JQuery-url]

[![Metro4][Metro4-url]][Metro4]

[![BCrypt.Net-Next][BCrypt.Net-Next-url]][BCrypt.Net-Next]

[![MVC][MVC-url]][MVC]

[![SignaturePad.js][SignaturePad.js-url]][SignaturePad.js]


<p  align="right">(<a  href="#readme-top">back to top</a>)</p>

  
  
  

<!-- GETTING STARTED -->

## Getting Started

  

To get a local copy up and running follow these steps.

  

### Download the Latest Release::

[![Stable-Release][Stable-Release]][Stable-Release]

### Prerequisites::
* Windows Server 2008 R2 / 2016 or Windows 10/11

* with Internet Information Service (IIS 7.5+)

* ASP.NET Web Application MVC5 ( .NET Framework 4.5 )

  
  

### Installations::

  

Steps for Developer Installation:

  

1. Install **Windows SQL Server** on your Machine with Instances:

  

* Database Engine Services

* Analysis Services

* Reporting Services

=> Follow [this](https://phoenixnap.com/kb/install-sql-server) by (Kovacevic, n.d. & https://phoenixnap.com/) for detailed instructions on `How to install SQL Server on Windows`

2. Set up **SRSS** (Windows Server only)
3. Install a **New Database** (called VCAS_DB) through **SSMS**, then restore the backup file `VCAS_DB.bak`, found in :
the latest release [![Stable-Release][Stable-Release]][Stable-Release]

  

4. Install **Visual Studio** (2019+) with the following extensions:

* Microsoft Reporting Services Project

* Microsoft RDLC Report Designer

 5. Download and Load the Source Files into Visual Studio

  

6. Open the **web.config** to update the below connection string configurations:

* {0} DB - Database Instance

* {1} DB - Database

* {2} DB - username

* {3} DB - password

* {4} SSRS Project - Report Folder

* {5} Active Directory url connection (Optional)

```xml

<connectionStrings>
<add  name="ModelContainer"  connectionString="metadata=res://*/Models.Model.csdl|res://*/Models.Model.ssdl|res://*/Models.Model.msl;provider=System.Data.SqlClient;provider connection string=&quot;data source={0};initial catalog={1};persist security info=True;user id={2};password={3};MultipleActiveResultSets=True;App=EntityFramework&quot;"  providerName="System.Data.EntityClient"  />
<add  name="ModelContainerUpload"  connectionString="Data Source={0};Initial Catalog={1};user id={2};password={3}"  />
<add  name="ReportPath"  connectionString="{4}"  /> 
<add  name="StoragePath" connectionString="\Content\Uploads\"/>
<add  name="ADConnectionString"  connectionString="{5}"  />

</connectionStrings>

```

  

7. Load and Test the project

  

### Installation (Regular User)::

If you would like to try the App, you can do so through the link below:

<a  href="https://app.vouchcast.com"><strong>Access Demo »»</strong></a>

  

<!-- Troubleshooting -->

## Troubleshooting
[TROUBLESHOOTING](https://github.com/lazarusvc/vcas_business/blob/master/TROUBLESHOOTING.md) guide
  

  
  
  

<!-- ROADMAP -->

## Roadmap

  

- [X] Implement Invoicing

- [ ] User portal - links from SupportDocs module

- [ ] Complete `Profit & Loss` Statement for Reports section

- [ ] Implement System Wide Notifications

- [ ] Implement Feature : Reconciliation

  
  

See the [open issues](https://github.com/Lazarusvc/vcas_open/issues) for a full list of proposed features (and known issues).

  

<p  align="right">(<a  href="#readme-top">back to top</a>)</p>

  
  
  

<!-- CONTRIBUTING -->

## Contributing

  

Contributions are what make the open source community such an amazing place to learn, inspire, and create. Any contributions you make are **greatly appreciated**.

Vouchcast Business is  **free, open-source software**  licensed under  **Commons Clause License**.

You can open issues for bugs you've found or features you think are missing. You can also submit pull requests to this repository. To get started, look at the  [CONTRIBUTING](https://github.com/lazarusvc/vcas_business/blob/master/CONTRIBUTING.md) guide.  

  

<p  align="right">(<a  href="#readme-top">back to top</a>)</p>

  
  
  

<!-- LICENSE -->

## License

  

Distributed under the **Commons Clause License**. See  [LICENSE](https://github.com/lazarusvc/vcas_business/blob/master/LICENSE.txt)  for more information.

  

<p  align="right">(<a  href="#readme-top">back to top</a>)</p>

  
  
  

<!-- CONTACT -->

## Contact

  

Austin Lazarus - [@lazarusvc](https://twitter.com/_ra_lazarus) - [contact@vouchcast.com](mailto:contact@vouchcast.com)
Project Link: [https://github.com/lazarusvc/vcas_business](https://github.com/lazarusvc/vcas_business)
Website Link: [https://vouchcast.com](https:vouchcast.com)

  
<p  align="right">(<a  href="#readme-top">back to top</a>)</p>
-- Edited with: [https://stackedit.io/](https://stackedit.io/)

  
  
  

<!-- MARKDOWN LINKS & IMAGES -->

<!-- https://www.markdownguide.org/basic-syntax/#reference-style-links -->

  

[Stable-Release]:https://badgen.net/github/release/lazarusvc/vcas_business
[Forks]:https://badgen.net/github/forks/lazarusvc/vcas_business
[License]:https://badgen.net/github/license/lazarusvc/vcas_business
[Contribution]:https://badgen.net/github/contributors/lazarusvc/vcas_business
[Check]:https://badgen.net/github/checks/lazarusvc/vcas_business
[Commits]:https://badgen.net/github/last-commit/lazarusvc/vcas_business
[Downloads]:https://badgen.net/github/assets-dl/lazarusvc/vcas_business  

[Bootstrap.com]: https://img.shields.io/badge/Bootstrap-563D7C?style=for-the-badge&logo=bootstrap&logoColor=white

[Bootstrap-url]: https://getbootstrap.com

[JQuery.com]: https://img.shields.io/badge/jQuery-0769AD?style=for-the-badge&logo=jquery&logoColor=white

[JQuery-url]: https://jquery.com

[Metro4]: https://metro.org.ua

[Metro4-url]: https://img.shields.io/badge/Metro4-Metro4?style=for-the-badge&logo=metro&logoColor=white&color=blue

[BCrypt.Net-Next]:https://github.com/BcryptNet/bcrypt.net

[BCrypt.Net-Next-url]:https://img.shields.io/badge/BCrypt.Net--Next-lightgrey?style=for-the-badge&logo=metro&logoColor=white&color=lightgrey

[MVC]:https://dotnet.microsoft.com/en-us/apps/aspnet/mvc

[MVC-url]:https://img.shields.io/badge/AspNet.Mvc-ff69b4?style=for-the-badge&logo=microsoft&logoColor=white&color=brightgreen

[SignaturePad.js]:https://github.com/szimek/signature_pad

[SignaturePad.js-url]:https://img.shields.io/badge/SignaturePad.js-yellowgreen?style=for-the-badge&logo=metro&logoColor=white&color=yellowgreen
