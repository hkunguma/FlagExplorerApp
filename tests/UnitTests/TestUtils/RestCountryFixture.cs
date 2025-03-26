using FlagExplorerApp.Application.Contracts.Interfaces;
using FlagExplorerApp.Application.Models;
using FlagExplorerApp.Common.Contracts;
using FlagExplorerApp.Common.Helpers;
using FlagExplorerApp.Infrastructure.Configuration;
using FlagExplorerApp.Infrastructure.Models;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.TestUtils
{
    public class RestCountryFixture : IDisposable
    {
        public RestCountryFixture() 
        {
            var restCountryConfig = new RestCountryConfig { RestCountryBaseUrl = "https://restcountries.com/v3.1/", GetAllEndpoint = "all", GetByNameEndpoint = "name" };
            MockRestCountryConfig = new Mock<IOptions<RestCountryConfig>>();
            MockRestCountryConfig.Setup(c => c.Value).Returns(restCountryConfig);

            MockCountryService = new Mock<ICountryService>();
            MockCountryService.Setup(c => c.GetAllCountries()).ReturnsAsync(GetAllCountriesData());
            MockCountryService.Setup(c => c.GetCountryByName(It.IsAny<string>())).ReturnsAsync((string name) => GetByNameCountryData(name));

            MockRestCountryService = new Mock<IRestCountryService>();
            MockRestCountryService.Setup(c => c.GetAllCountries()).ReturnsAsync(GetAllCountriesData());
            MockRestCountryService.Setup(c => c.GetCountryByName(It.IsAny<string>())).ReturnsAsync((string name) => GetByNameCountryData(name));

            MockHttpClientGetAll = new Mock<HttpClientHelper<IEnumerable<CountryData>>>();
            MockHttpClientGetAll.Setup(c => c.GetAsync("all")).ReturnsAsync(GetAllExternalRestCountriesData);

            MockHttpClientGetByName = new Mock<HttpClientHelper<CountryData>>();
            MockHttpClientGetByName.Setup(c => c.GetAsync("name/Hungary")).ReturnsAsync(GetByNameExternalRestCountriesData);

            MockHttpMessageHandler = new Mock<HttpMessageHandler>();
            MockHttpMessageHandler.Protected().Setup<Task<HttpResponseMessage>>(
                        "SendAsync",
                        ItExpr.IsAny<HttpRequestMessage>(),
                        ItExpr.IsAny<CancellationToken>()
                      )
                      .ReturnsAsync(new HttpResponseMessage
                      {
                          StatusCode = HttpStatusCode.OK,
                          Content = new StringContent(
                                "[{\"name\": {\"common\": \"South Georgia\", \"official\": \"South Georgia and the South Sandwich Islands\", \"nativeName\": { \"eng\": { \"official\": \"South Georgia and the South Sandwich Islands\", \"common\": \"South Georgia\" } } }, \"tld\": [ \".gs\" ], \"cca2\": \"GS\", \"ccn3\": \"239\",  \"cca3\": \"SGS\",  \"independent\": false,  \"status\": \"officially-assigned\",  \"unMember\": false,  \"currencies\": {  \"SHP\": {  \"name\": \"Saint Helena pound\", \"symbol\": \"£\" }  },  \"idd\": {  \"root\": \"+5\", \"suffixes\": [  \"00\"  ]  },  \"capital\": [  \"King Edward Point\"  ],  \"altSpellings\": [   \"GS\",  \"South Georgia and the South Sandwich Islands\"  ],  \"region\": \"Antarctic\",  \"languages\": {  \"eng\": \"English\"  },  \"translations\": {  \"ara\": {   \"official\": \"جورجيا الجنوبية وجزر ساندوتش الجنوبية\",   \"common\": \"جورجيا الجنوبية\"  },  \"bre\": {  \"official\": \"Georgia ar Su hag Inizi Sandwich ar Su\",  \"common\": \"Georgia ar Su hag Inizi Sandwich ar Su\"   },   \"ces\": {   \"official\": \"Jižní Georgie a Jižní Sandwichovy ostrovy\",   \"common\": \"Jižní Georgie a Jižní Sandwichovy ostrovy\"  },  \"cym\": { \"official\": \"South Georgia and the South Sandwich Islands\",   \"common\": \"South Georgia\"  },  \"deu\": {   \"official\": \"Südgeorgien und die Südlichen Sandwichinseln\",  \"common\": \"Südgeorgien und die Südlichen Sandwichinseln\" },  \"est\": {   \"official\": \"Lõuna-Georgia ja Lõuna-Sandwichi saared\",  \"common\": \"Lõuna-Georgia ja Lõuna-Sandwichi saared\"   },   \"fin\": {  \"official\": \"Etelä-Georgia ja Eteläiset Sandwichsaaret\",  \"common\": \"Etelä-Georgia ja Eteläiset Sandwichsaaret\"  },   \"fra\": { \"official\": \"Géorgie du Sud et les îles Sandwich du Sud\",  \"common\": \"Géorgie du Sud-et-les Îles Sandwich du Sud\"   },   \"hrv\": {    \"official\": \"Južna Džordžija i Otoci Južni Sendvič\",    \"common\": \"Južna Georgija i otočje Južni Sandwich\"   },   \"hun\": {    \"official\": \"Déli-Georgia és Déli-Sandwich-szigetek\",    \"common\": \"Déli-Georgia és Déli-Sandwich-szigetek\"   },   \"ita\": {    \"official\": \"Georgia del Sud e isole Sandwich del Sud\",    \"common\": \"Georgia del Sud e Isole Sandwich Meridionali\"   },   \"jpn\": {    \"official\": \"サウスジョージア·サウスサンドウィッチ諸島\",   \"common\": \"サウスジョージア・サウスサンドウィッチ諸島\"   },   \"kor\": {    \"official\": \"조지아\",    \"common\": \"조지아\"   },   \"nld\": {    \"official\": \"Zuid-Georgië en de Zuidelijke Sandwich-eilanden\",    \"common\": \"Zuid-Georgia en Zuidelijke Sandwicheilanden\"   },   \"per\": {    \"official\": \"جزایر جورجیای جنوبی و ساندویچ جنوبی\",    \"common\": \"جزایر جورجیای جنوبی و ساندویچ جنوبی\"   },   \"pol\": {    \"official\": \"Georgia Południowa i Sandwich Południowy\",    \"common\": \"Georgia Południowa i Sandwich Południowy\"   },   \"por\": {    \"official\": \"Geórgia do Sul e Sandwich do Sul\",    \"common\": \"Ilhas Geórgia do Sul e Sandwich do Sul\"   },   \"rus\": {    \"official\": \"Южная Георгия и Южные Сандвичевы острова\",    \"common\": \"Южная Георгия и Южные Сандвичевы острова\"   },   \"slk\": {    \"official\": \"Južná Georgia a Južné Sandwichove ostrovy\",    \"common\": \"Južná Georgia a Južné Sandwichove ostrovy\"   },   \"spa\": {    \"official\": \"Georgia del Sur y las Islas Sandwich del Sur\",    \"common\": \"Islas Georgias del Sur y Sandwich del Sur\"   },   \"srp\": {    \"official\": \"Јужна Џорџија и Јужна Сендвичка Острва\",    \"common\": \"Јужна Џорџија и Јужна Сендвичка Острва\"   },   \"swe\": {    \"official\": \"Sydgeorgien\",    \"common\": \"Sydgeorgien\"   },   \"tur\": {    \"official\": \"Güney Georgia ve Güney Sandwich Adaları\",    \"common\": \"Güney Georgia ve Güney Sandwich Adaları\"   },   \"urd\": {    \"official\": \"جنوبی جارجیا و جزائر جنوبی سینڈوچ\",    \"common\": \"جنوبی جارجیا\"   },   \"zho\": {    \"official\": \"南乔治亚岛和南桑威奇群岛\",    \"common\": \"南乔治亚\"   }  },  \"latlng\": [   -54.5,   -37.0  ],  \"landlocked\": false,  \"area\": 3903.0,  \"demonyms\": {   \"eng\": {    \"f\": \"South Georgian South Sandwich Islander\",    \"m\": \"South Georgian South Sandwich Islander\"   }  },  \"flag\": \"🇬🇸\",  \"maps\": {   \"googleMaps\": \"https://goo.gl/maps/mJzdaBwKBbm2B81q9\",   \"openStreetMaps\": \"https://www.openstreetmap.org/relation/1983629\"  },  \"population\": 30,  \"car\": {   \"signs\": [    \"\"   ],   \"side\": \"right\"  },  \"timezones\": [   \"UTC-02:00\"  ],  \"continents\": [   \"Antarctica\"  ],  \"flags\": {   \"png\": \"https://flagcdn.com/w320/gs.png\",   \"svg\": \"https://flagcdn.com/gs.svg\"  },  \"coatOfArms\": {},  \"startOfWeek\": \"monday\",  \"capitalInfo\": {   \"latlng\": [    -54.28,    -36.5   ]  }\r\n },\r\n {  \"name\": {   \"common\": \"Hungary\",   \"official\": \"Hungary\",   \"nativeName\": {    \"hun\": {     \"official\": \"Magyarország\",     \"common\": \"Magyarország\"    }   }  },  \"tld\": [   \".hu\"  ],  \"cca2\": \"HU\",  \"ccn3\": \"348\",  \"cca3\": \"HUN\",  \"cioc\": \"HUN\",  \"independent\": true,  \"status\": \"officially-assigned\",  \"unMember\": true,  \"currencies\": {   \"HUF\": {    \"name\": \"Hungarian forint\",    \"symbol\": \"Ft\"   }  },  \"idd\": {   \"root\": \"+3\",   \"suffixes\": [    \"6\"   ]  },  \"capital\": [   \"Budapest\"  ],  \"altSpellings\": [   \"HU\"  ],  \"region\": \"Europe\",  \"subregion\": \"Central Europe\",  \"languages\": {   \"hun\": \"Hungarian\"  },  \"translations\": {   \"ara\": {    \"official\": \"الجمهورية المجرية\",    \"common\": \"المجر\"   },   \"bre\": {    \"official\": \"Hungaria\",    \"common\": \"Hungaria\"   },   \"ces\": {    \"official\": \"Maďarsko\",    \"common\": \"Maďarsko\"   },   \"cym\": {    \"official\": \"Hungary\",    \"common\": \"Hungary\"   },   \"deu\": {    \"official\": \"Ungarn\",    \"common\": \"Ungarn\"   },   \"est\": {    \"official\": \"Ungari\",    \"common\": \"Ungari\"   },   \"fin\": {    \"official\": \"Unkari\",    \"common\": \"Unkari\"   },   \"fra\": {    \"official\": \"Hongrie\",    \"common\": \"Hongrie\"   },   \"hrv\": {    \"official\": \"Madžarska\",    \"common\": \"Mađarska\"   },   \"hun\": {    \"official\": \"Magyarország\",    \"common\": \"Magyarország\"   },   \"ita\": {    \"official\": \"Ungheria\",    \"common\": \"Ungheria\"   },   \"jpn\": {    \"official\": \"ハンガリー\",    \"common\": \"ハンガリー\"   },   \"kor\": {    \"official\": \"헝가리\",    \"common\": \"헝가리\"   },   \"nld\": {    \"official\": \"Hongarije\",    \"common\": \"Hongarije\"   },   \"per\": {    \"official\": \"مجارستان\",    \"common\": \"مجارستان\"   },   \"pol\": {    \"official\": \"Węgry\",    \"common\": \"Węgry\"   },   \"por\": {    \"official\": \"Hungria\",    \"common\": \"Hungria\"   },   \"rus\": {    \"official\": \"Венгрия\",    \"common\": \"Венгрия\"   },   \"slk\": {    \"official\": \"Maďarsko\",    \"common\": \"Maďarsko\"   },   \"spa\": {    \"official\": \"Hungría\",    \"common\": \"Hungría\"   },   \"srp\": {    \"official\": \"Мађарска\",    \"common\": \"Мађарска\"   },   \"swe\": {    \"official\": \"Ungern\",    \"common\": \"Ungern\"   },   \"tur\": {    \"official\": \"Macaristan\",    \"common\": \"Macaristan\"   },   \"urd\": {    \"official\": \"مجارستان\",    \"common\": \"مجارستان\"   },   \"zho\": {    \"official\": \"匈牙利\",    \"common\": \"匈牙利\"   }  },  \"latlng\": [   47.0,   20.0  ],  \"landlocked\": true,  \"borders\": [   \"AUT\",   \"HRV\",   \"ROU\",   \"SRB\",   \"SVK\",   \"SVN\",   \"UKR\"  ],  \"area\": 93028.0,  \"demonyms\": {   \"eng\": {    \"f\": \"Hungarian\",    \"m\": \"Hungarian\"   },   \"fra\": {    \"f\": \"Hongroise\",    \"m\": \"Hongrois\"   }  },  \"flag\": \"🇭🇺\",  \"maps\": {   \"googleMaps\": \"https://goo.gl/maps/9gfPupm5bffixiFJ6\",   \"openStreetMaps\": \"https://www.openstreetmap.org/relation/21335\"  },  \"population\": 9749763,  \"gini\": {   \"2018\": 29.6  },  \"fifa\": \"HUN\",  \"car\": {   \"signs\": [    \"H\"   ],   \"side\": \"right\"  },  \"timezones\": [   \"UTC+01:00\" ], \"continents\": [ \"Europe\" ], \"flags\": { \"png\": \"https://flagcdn.com/w320/hu.png\", \"svg\": \"https://flagcdn.com/hu.svg\", \"alt\": \"The flag of Hungary is composed of three equal horizontal bands of red, white and green.\" }, \"coatOfArms\": { \"png\": \"https://mainfacts.com/media/images/coats_of_arms/hu.png\", \"svg\": \"https://mainfacts.com/media/images/coats_of_arms/hu.svg\" }, \"startOfWeek\": \"monday\", \"capitalInfo\": { \"latlng\": [ 47.5,  19.08 ] }, \"postalCode\": { \"format\": \"####\", \"regex\": \"^(\\\\d{4})$\"} }]"
                              )
                      });
        }

        public Mock<IOptions<RestCountryConfig>> MockRestCountryConfig { get; set; }
        public Mock<ICountryService> MockCountryService { get; set; }
        public Mock<IRestCountryService> MockRestCountryService { get; set; }
        internal Mock<HttpClientHelper<IEnumerable<CountryData>>> MockHttpClientGetAll { get; set; }
        internal Mock<HttpClientHelper<CountryData>> MockHttpClientGetByName { get; set; }
        public Mock<HttpMessageHandler> MockHttpMessageHandler { get; set; }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        private IList<Country> GetAllCountriesData()
        {
            var countries = new List<Country>()
            {
                new Country{ Name = "Hungary", Flag = "https://flagcdn.com/w320/hu.png" }, 
                new Country{ Name = "South Georgia", Flag = "https://flagcdn.com/w320/gs.png" }
            };
            return countries;
        }

        private CountryDetail GetByNameCountryData(string name)
        {
            var countries = new List<CountryDetail>()
            {
                new CountryDetail{ Name = "Hungary", Flag = "https://flagcdn.com/w320/hu.png", Population = 3211, Capital = "Budapest" },
                new CountryDetail{ Name = "South Georgia", Flag = "https://flagcdn.com/w320/gs.png", Population = 4211, Capital = "King Edward Point" }
            };

            return countries.FirstOrDefault(x => x.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        private IEnumerable<CountryData> GetAllExternalRestCountriesData()
        {
            var countries = new List<CountryData>()
            {
                new CountryData {
                    Name = new NameData {
                        Common = "Hungary",
                        Official = "",
                        NativeName = new Dictionary<string, object> {
                            { "hu", new Dictionary<string, string> { { "", ""} } }
                        }
                    },
                    Flags = new Dictionary<string, string> {
                        { "png", "https://flagcdn.com/w320/hu.png" },
                        { "svg", "https://flagcdn.com/hu.svg" },
                        { "alt", "The flag of Hungary is composed of three equal horizontal bands of red, white and green." }
                    },
                    Population = 3211,
                    Capital = new List<string> { "Budapest" }
                },
                new CountryData {
                    Name = new NameData {
                        Common = "South Georgia",
                        Official = "",
                        NativeName = new Dictionary<string, object> {
                            { "eng", new Dictionary<string, string> { { "", ""} } }
                        }
                    },
                    Flags = new Dictionary<string, string> {
                        { "png", "https://flagcdn.com/w320/gs.png" },
                        { "svg", "" },
                        { "alt", "" }
                    },
                    Population = 3211,
                    Capital = new List<string> { "King Edward Point" }
                }
            };

            return countries;
        }

        private CountryData GetByNameExternalRestCountriesData()
        {
            return new CountryData
            {
                Name = new NameData
                {
                    Common = "Hungary",
                    Official = "",
                    NativeName = new Dictionary<string, object> {
                            { "hu", new Dictionary<string, string> { { "", ""} } }
                        }
                },
                Flags = new Dictionary<string, string> {
                        { "png", "https://flagcdn.com/w320/hu.png" },
                        { "svg", "https://flagcdn.com/hu.svg" },
                        { "alt", "The flag of Hungary is composed of three equal horizontal bands of red, white and green." }
                    },
                Population = 3211,
                Capital = new List<string> { "Budapest" }
            };
        }
    }
}
