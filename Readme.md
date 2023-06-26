<a name="readme-top"></a>

<!-- PROJECT SHIELDS -->
<!--
*** I'm using markdown "reference style" links for readability.
*** Reference links are enclosed in brackets [ ] instead of parentheses ( ).
*** See the bottom of this document for the declaration of the reference variables
*** for contributors-url, forks-url, etc. This is an optional, concise syntax you may use.
*** https://www.markdownguide.org/basic-syntax/#reference-style-links
-->


[![Stable-Release][Stable-Release]][Stable-Release] [![Forks][Forks]][Forks] [![Contribution][Contribution]][Contribution] [![Check][Check]][Check] [![License][License]][License]

<!-- PROJECT LOGO -->
<br />
<div align="center">
  <a href="https://github.com/Lazarusvc/vcas_open">
    <img src="http://vouchcast.com/wp-content/uploads/2022/10/vouchcast_screenshots2-1024x648.png" alt="VC Accounting Software">
  </a>

<h1 align="center">VouchCast (VC) Accounting Software</h1>
  <p align="center">
    Open Source Self-Hosted Accounting Software based on ASP.NET (MVC5) web technology
    <br />
    <a href="https://app.vouchcast.com"><strong>Access Demo »</strong></a>
    <br />
    <br />
    <a href="https://github.com/Lazarusvc/vcas_open/issues">Report Bug</a>
    ·
    <a href="https://github.com/Lazarusvc/vcas_open/issues">Request Feature</a>
  </p>
</div>



<!-- TABLE OF CONTENTS -->
<details>
  <summary>Table of Contents</summary>
  <ol>
    <li>
      <a href="#about-the-project">About The Project</a>
      <ul>
        <li><a href="#built-with">Built With</a></li>
      </ul>
    </li>
    <li>
      <a href="#getting-started">Getting Started</a>
      <ul>
        <li><a href="#prerequisites">Prerequisites</a></li>
        <li><a href="#installation">Installation</a></li>
      </ul>
    </li>
    <li><a href="#troubleshooting">Troubleshooting</a></li>
    <li><a href="#roadmap">Roadmap</a></li>
    <li><a href="#contributing">Contributing</a></li>
    <li><a href="#license">License</a></li>
    <li><a href="#contact">Contact</a></li>
  </ol>
</details>



<!-- ABOUT THE PROJECT -->
## About The Project

The VouchCast software is a Progressive Web Application built on Microsoft ASP.NET MVC5 Web Application that allow SMEs to manage their Financials through a streamlined, simple and clean interface online. The app can be hosted and accessed on a PC, Mobile (Android / iOS), Server or any Windows based Computer with the necessary configurations. 

The App has some standard features for accounting such as:

- Receivables (Receipt Capture)
- Payables (Issue Payments) 
- Charts of Accounts (Debit Account Mgmt)
- Custom Reporting (Sales Report, Profit & Loss Statement)
- Inventory Management
- Customer Mgmt
- Ordering
- Plus more

<p align="right">(<a href="#readme-top">back to top</a>)</p>



### Built With

VC Accounting is developed with these dependencies:

[![Bootstrap][Bootstrap.com]][Bootstrap-url]
[![JQuery][JQuery.com]][JQuery-url]
[![Metro4][Metro4-url]][Metro4]
[![BCrypt.Net-Next][BCrypt.Net-Next-url]][BCrypt.Net-Next]
[![MVC][MVC-url]][MVC]
[![SignaturePad.js][SignaturePad.js-url]][SignaturePad.js]


<p align="right">(<a href="#readme-top">back to top</a>)</p>



<!-- GETTING STARTED -->
## Getting Started

To get a local copy up and running follow these simple steps.

### Download the Latest Release::
[![Stable-Release][Stable-Release]][Stable-Release]


### Prerequisites::

tested technologies --

* Windows Server 2008 R2 / 2016 
	* with Internet Information Service (IIS 7.5+)
* ASP.NET Web Application MVC5 ( .NET Framework 4.5 )


### Installation (Developers)::

Steps for Developer Installation:

1. Install **Windows SQL Server** on your Machine with Instances:

	*	Database Engine Services
	*	Analysis Services
	*	Reporting Services
	
=> Follow [this](https://phoenixnap.com/kb/install-sql-server) by (Kovacevic, n.d. & https://phoenixnap.com/) for detailed instructions on `How to install SQL Server on Windows 10`

2.	 Set up **SRSS**

3. Install a **New Database** (called VCAS) through SSMS, then restore the backup file `VCAS.bak` - file can be found in :
	=>  Our Latest Release [![Stable-Release][Stable-Release]][Stable-Release]

4. Install **Visual Studio** (2019 Recommended) with the following extensions:
	* Microsoft Analysis Services Project
	* Microsoft Reporting Services Project
	* Microsoft RDLC Report Designer

5.  Download and Load the Source Files into Visual Studio

6. Open the **web.config** to update the below connection string configurations:
	*	{0} DB - Database Instance
	*	{1} DB - Database 
	*	{2} DB - username
	*	{3} DB - password
	*	{4} SSRS Project - Report Folder
	*	{5} Active Directory url connection
```xml
  <connectionStrings>
    <add name="ModelContainer" connectionString="metadata=res://*/Models.Model.csdl|res://*/Models.Model.ssdl|res://*/Models.Model.msl;provider=System.Data.SqlClient;provider connection string=&quot;data source={0};initial catalog={1};persist security info=True;user id={2};password={3};MultipleActiveResultSets=True;App=EntityFramework&quot;" providerName="System.Data.EntityClient" />
    <add name="ModelContainerUpload" connectionString="Data Source={0};Initial Catalog={1};user id={2};password={3}" />
	<add name="ReportPath" connectionString="/{4}/" />
	<add name="StoragePath" connectionString= "\Content\Uploads\"/>
	<add name="ADConnectionString" connectionString="{5}" />  
  </connectionStrings>
```

7. Load and Test the project

### Installation (Regular User)::
1. The Progressive Web App can be accessed here:
-	<a href="https://app.vouchcast.com"><strong>Access Demo »</strong></a>

<!-- Troubleshooting -->
## Troubleshooting

### Report Not Loading - Error
If loading up your reports show the below error, SSRS report permissions needs to be configured.

### Files unable to upload - Error



<!-- ROADMAP -->
## Roadmap

- [X] Implement Invoicing
- [ ] User portal - links from SupportDocs module
- [ ] Complete `Profit & Loss` Statement for Reports section
- [ ] Implement System Wide Notifications
- [ ] Implement Feature : Reconciliation


See the [open issues](https://github.com/Lazarusvc/vcas_open/issues) for a full list of proposed features (and known issues).

<p align="right">(<a href="#readme-top">back to top</a>)</p>



<!-- CONTRIBUTING -->
## Contributing

Contributions are what make the open source community such an amazing place to learn, inspire, and create. Any contributions you make are **greatly appreciated**.

If you have a suggestion that would make this better, please fork the repo and create a pull request. You can also simply open an issue with the tag "enhancement".
Don't forget to give the project a star! Thanks again!

1. Fork the Project
2. Create your Feature Branch (`git checkout -b feature/AmazingFeature`)
3. Commit your Changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the Branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

<p align="right">(<a href="#readme-top">back to top</a>)</p>



<!-- LICENSE -->
## License

Distributed under the MIT License. See `LICENSE.txt` for more information.

<p align="right">(<a href="#readme-top">back to top</a>)</p>



<!-- CONTACT -->
## Contact

Austin Lazarus - [@_ra_lazarus](https://twitter.com/_ra_lazarus) - [austin.lazarus@gmail.com](mailto:austin.lazarus@gmail.com)

Project Link: [https://github.com/Lazarusvc/vcas_open](https://github.com/Lazarusvc/vcas_open)

Website Link: [https://vouchcast.com](https:vouchcast.com)

<p align="right">(<a href="#readme-top">back to top</a>)</p>

-- Edited with: [https://stackedit.io/](https://stackedit.io/)



<!-- MARKDOWN LINKS & IMAGES -->
<!-- https://www.markdownguide.org/basic-syntax/#reference-style-links -->

[Stable-Release]:https://badgen.net/github/release/lazarusvc/vcas_open/stable
[Forks]:https://badgen.net/github/forks/lazarusvc/vcas_open
[License]:https://badgen.net/github/license/lazarusvc/vcas_open
[Contribution]:https://badgen.net/github/contributors/lazarusvc/vcas_open
[Check]:https://badgen.net/github/checks/lazarusvc/vcas_open/main

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