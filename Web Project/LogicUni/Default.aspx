<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>


<asp:Content ID="indexFeatured" ContentPlaceHolderID="FeaturedContent" runat="server">
    <div class="intro-header">
        <section class="container">
            <div class="jumbotron">
                <div class="row">
                    <div class="col-lg-12">
                        <div class="header">
                            <h1>LOGIC UNIVERSITY</h1>
                            <h3>Stationery Inventory Store
                            </h3>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
</asp:Content>

<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent" runat="server">
        <a id="about"></a>
        <div class="content-section-a">
            <div class="container">

                <h3 style="text-align: center">Our University's Profile</h3>
                <div class="col-lg-6">
                    <h4><span class="glyphicon glyphicon-info-sign" aria-hidden="true"></span>About our university</h4>
                    <p>
                        Our university is a leading global university centered in Asia.
                    We aspire to influence the future. By changing the way people think and do things through education, research, and service.
            <a href="http://www.nus.edu.sg/about">Learn more…</a>
                    </p>
                </div>
                <div class="col-lg-6">
                    <h4><span class="glyphicon glyphicon-info-sign" aria-hidden="true"></span>About our stationery store</h4>
                    <p>
                        Our stationery store provides stationeries required for all the departments in our university. We process, and deliver all the stationeries request in a short time, satisfying the needs of our departments. 
                <a href="http://www.nus.edu.sg/about">Learn more…</a>
                    </p>
                </div>
            </div>
        </div>
        <div class="container-fluid">
            <div class="imageblock">
                <img src="Images/headerbg.jpg" class="img-responsive" alt="Cinque Terre">
            </div>
        </div>
    <a id="contact" />
    <div class="content-section-b">
        <div class="container">

            <div class="col-lg-5 col-sm-6">
                <h5><span class="glyphicon glyphicon-send" aria-hidden="true"></span>&nbsp;Contact Us</h5>

                <p><a href="tel:+6565166666"><span class="glyphicon glyphicon-phone-alt" aria-hidden="true"></span>&nbsp;+65 6516 6666 </a></p>

                <p><a href="mailto:enquiry@nus.edu.sg"><span class="glyphicon glyphicon-envelope" aria-hidden="true"></span>&nbsp;enquiry@nus.edu.sg</a></p>

                 <p>
                    <a href="https://www.google.com.sg/maps/place/21+Lower+Kent+Ridge+Rd,+Singapore+119077/@1.2971342,103.7777567,17z/data=!3m1!4b1!4m2!3m1!1s0x31da1a57a5e7f729:0x5b73066b368df5e9?hl=en" target="_blank"><span class="glyphicon glyphicon-map-marker" aria-hidden="true"></span>&nbsp;21 Lower Kent Ridge Road
Singapore 119077</a>
                </p>

            </div>
            <div class="col-lg-5 col-lg-offset-2 col-sm-6">
                <h5><span class="glyphicon glyphicon-globe" aria-hidden="true"></span>&nbsp;Find us on:</h5>

                <a href="#">
                    <img src="Images/facebook.png" alt="facebook"/></a>
                <a href="#">
                    <img src="Images/instagram19.png" alt="instagram"/></a>

                <a href="#">
                    <img src="Images/twitter.png" alt="twitter"/></a>
                <a href="#">
                    <img src="Images/youtube30.png" alt="youtube"/></a>

            </div>
        </div>

    </div>
</asp:Content>
