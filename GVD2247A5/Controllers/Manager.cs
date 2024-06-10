using AutoMapper;
using GVD2247A5.Data;
using GVD2247A5.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.UI.WebControls;
using static System.Net.WebRequestMethods;

// ************************************************************************************
// WEB524 Project Template V2 == 2241-32263813-7f67-49ff-8cf4-5afba2983ccc
//
// By submitting this assignment you agree to the following statement.
// I declare that this assignment is my own work in accordance with the Seneca Academic
// Policy. No part of this assignment has been copied manually or electronically from
// any other source (including web sites) or distributed to other students.
// ************************************************************************************

namespace GVD2247A5.Controllers
{
    public class Manager
    {
        // Reference to the data context
        private ApplicationDbContext ds = new ApplicationDbContext();

        // AutoMapper instance
        public IMapper mapper;

        // Request user property...

        // Backing field for the property
        private RequestUser _user;

        // Getter only, no setter
        public RequestUser User
        {
            get
            {
                // On first use, it will be null, so set its value
                if (_user == null)
                {
                    _user = new RequestUser(HttpContext.Current.User as ClaimsPrincipal);
                }
                return _user;
            }
        }

        // Default constructor...
        public Manager()
        {
            // If necessary, add constructor code here

            // Configure the AutoMapper components
            var config = new MapperConfiguration(cfg =>
            {
                // Define the mappings below, for example...
                // cfg.CreateMap<SourceType, DestinationType>();
                // cfg.CreateMap<Product, ProductBaseViewModel>();

                cfg.CreateMap<Models.RegisterViewModel, Models.RegisterViewModelForm>();

                cfg.CreateMap<Actor, ActorBaseViewModel>();
                cfg.CreateMap<Actor, ActorAddViewModel>();
                cfg.CreateMap<Actor, ActorWithShowInfoViewModel>();
                cfg.CreateMap<ActorAddViewModel, Actor>();
                cfg.CreateMap<ActorMediaItem, ActorMediaItemBaseViewModel>();
                cfg.CreateMap<ActorMediaItem, ActorMediaItemWithContentViewModel>();
                cfg.CreateMap<ActorMediaItem, Actor>();
                cfg.CreateMap<EpisodeAddViewModel, Episode>();
                cfg.CreateMap<Episode, EpisodeBaseViewModel>();
                cfg.CreateMap<Episode, EpisodeVideoViewModel>();
                cfg.CreateMap<Episode, EpisodeWithShowNameViewModel>();
                cfg.CreateMap<Genre, GenreBaseViewModel>();
                cfg.CreateMap<ShowAddViewModel, Show>();
                cfg.CreateMap<Show, ShowBaseViewModel>();
                cfg.CreateMap<Show, ShowWithInfoViewModel>();
            });

            mapper = config.CreateMapper();

            // Turn off the Entity Framework (EF) proxy creation features
            // We do NOT want the EF to track changes - we'll do that ourselves
            ds.Configuration.ProxyCreationEnabled = false;

            // Also, turn off lazy loading...
            // We want to retain control over fetching related objects
            ds.Configuration.LazyLoadingEnabled = false;
        }

        // Add your methods below and call them from controllers. Ensure that your methods accept
        // and deliver ONLY view model objects and collections. When working with collections, the
        // return type is almost always IEnumerable<T>.
        //
        // Remember to use the suggested naming convention, for example:
        // ProductGetAll(), ProductGetById(), ProductAdd(), ProductEdit(), and ProductDelete().
        public bool LoadGenres()
        {
            if (ds.Genres.Count() > 0) { return false; }

            ds.Genres.Add(new Genre { Name = "Action" });
            ds.Genres.Add(new Genre { Name = "Adventure" });
            ds.Genres.Add(new Genre { Name = "Apocalyptic" });
            ds.Genres.Add(new Genre { Name = "Crime" });
            ds.Genres.Add(new Genre { Name = "Drama" });
            ds.Genres.Add(new Genre { Name = "Fantasy" });
            ds.Genres.Add(new Genre { Name = "Mystery" });
            ds.Genres.Add(new Genre { Name = "Romance" });
            ds.Genres.Add(new Genre { Name = "Sport" });
            ds.Genres.Add(new Genre { Name = "Thriller" });

            ds.SaveChanges();
            return true;
        }

        public bool LoadActors()
        {
            if (ds.Actors.Count() > 0) { return false; }

            ds.Actors.Add(new Actor
            {
                Name = "José Pedro Balmaceda Pascal",
                AlternateName = "Pedro Pascal",
                BirthDate = new DateTime(1975, 9, 9),
                Height = 1.8,
                ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/c/c5/Pedro_Pascal_by_Gage_Skidmore.jpg/800px-Pedro_Pascal_by_Gage_Skidmore.jpg",
                Executive = HttpContext.Current.User.Identity.Name
            }) ;
            ds.Actors.Add(new Actor
            {
                Name = "Christopher Julius Rock",
                AlternateName = "Chris Rock",
                BirthDate = new DateTime(1965, 2, 7),
                Height = 1.78,
                ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/7/71/Chris_Rock_-_Orpheum_Theatre_Minneapolis_3_17_%2833336280016%29_%28cropped%29.jpg/800px-Chris_Rock_-_Orpheum_Theatre_Minneapolis_3_17_%2833336280016%29_%28cropped%29.jpg",
                Executive = HttpContext.Current.User.Identity.Name
            });
            ds.Actors.Add(new Actor
            {
                Name = "Cameron Michelle Diaz",
                AlternateName = "Cameron Diaz",
                BirthDate = new DateTime(1972, 8, 30),
                Height = 1.74,
                ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/8/81/Cameron_Diaz_WE_2012_Shankbone_4.JPG/800px-Cameron_Diaz_WE_2012_Shankbone_4.JPG",
                Executive = HttpContext.Current.User.Identity.Name
            });

            ds.SaveChanges();

            return true;
        }

        public bool LoadShows()
        {
            if (ds.Shows.Count() > 0) { return false; }

            var pedroPascal = ds.Actors.SingleOrDefault(a => a.AlternateName == "Pedro Pascal");

            if (pedroPascal == null) { return false; }

            ds.Shows.Add(new Show
            {
                Actors = new Actor[] { pedroPascal },
                Name = "The Mandalorian",
                Genre = "Adventure",
                ReleaseDate = new DateTime(2019, 11, 12),
                ImageUrl = "https://media.comicbook.com/2019/04/the-mandalorian-logo-1167051.jpeg",
                Coordinator = HttpContext.Current.User.Identity.Name
            });
            ds.Shows.Add(new Show
            {
                Actors = new Actor[] { pedroPascal },
                Name = "The Last of Us",
                Genre = "Apocalyptic",
                ReleaseDate = new DateTime(2023, 3, 14),
                ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/a/ac/The_Last_of_Us_%28TV_series%29_logo.svg/512px-The_Last_of_Us_%28TV_series%29_logo.svg.png",
                Coordinator = HttpContext.Current.User.Identity.Name
            });

            ds.SaveChanges();

            return true;
        }

        public bool LoadEpisodes()
        {
            if (ds.Episodes.Count() > 0) { return false; }

            var theMandalorian = ds.Shows.SingleOrDefault(s => s.Name == "The Mandalorian");
            if (theMandalorian == null) { return false; }

            var lastOfUs = ds.Shows.SingleOrDefault(s => s.Name == "The Last of Us");
            if (lastOfUs == null) { return false; }

            ds.Episodes.Add(new Episode
            {
                ShowId = theMandalorian.Id,
                AirDate = new DateTime(2019, 11, 12),
                Clerk = HttpContext.Current.User.Identity.Name,
                EpisodeNumber = 1,
                Genre = theMandalorian.Genre,
                ImageUrl = "https://m.media-amazon.com/images/M/MV5BNTViYzhmZTAtY2MzYi00ZTk1LTg5OGItYzc4MjBlYzlkNzU0XkEyXkFqcGdeQXVyMTkxNjUyNQ@@._V1_.jpg",
                Name = "Chapter 1: The Mandalorian",
                SeasonNumber = 1
            });

            ds.Episodes.Add(new Episode
            {
                ShowId = theMandalorian.Id,
                AirDate = new DateTime(2019, 11, 15),
                Clerk = HttpContext.Current.User.Identity.Name,
                EpisodeNumber = 2,
                Genre = theMandalorian.Genre,
                ImageUrl = "https://i.insider.com/5dcda8633afd3713ce0b6f12?width=700",
                Name = "Chapter 2: The Child",
                SeasonNumber = 1,
            });

            ds.Episodes.Add(new Episode
            {
                ShowId = theMandalorian.Id,
                AirDate = new DateTime(2019, 11, 22),
                Clerk = HttpContext.Current.User.Identity.Name,
                EpisodeNumber = 3,
                Genre = theMandalorian.Genre,
                ImageUrl = "https://m.media-amazon.com/images/M/MV5BN2FiMTc4NTktZDdlNS00ODM1LTlmYjgtZGEyMjU1ZGNkMjY0XkEyXkFqcGdeQXVyMTkxNjUyNQ@@._V1_.jpg",
                Name = "Chapter 3: The Sin",
                SeasonNumber = 1,
            });

            ds.Episodes.Add(new Episode
            {
                ShowId = lastOfUs.Id,
                AirDate = new DateTime(2023, 1, 15),
                Clerk = HttpContext.Current.User.Identity.Name,
                EpisodeNumber = 1,
                Genre = lastOfUs.Genre,
                ImageUrl = "https://i.guim.co.uk/img/media/21060b8bd2972b5af8845af0b50d8be15d674e58/0_0_6000_4000/master/6000.jpg?width=700&quality=85&auto=format&fit=max&s=a3cb120546f906b89aee0760deead708",
                Name = "When You're Lost in the Darkness",
                SeasonNumber = 1,
            });

            ds.Episodes.Add(new Episode
            {
                ShowId = lastOfUs.Id,
                AirDate = new DateTime(2023, 1, 22),
                Clerk = HttpContext.Current.User.Identity.Name,
                EpisodeNumber = 2,
                Genre = lastOfUs.Genre,
                ImageUrl = "https://static0.gamerantimages.com/wordpress/wp-content/uploads/2023/01/the-last-of-us-episode-2-ellie-and-joel.jpg",
                Name = "Infected",
                SeasonNumber = 1,
            });

            ds.Episodes.Add(new Episode
            {
                ShowId = lastOfUs.Id,
                AirDate = new DateTime(2023, 1, 29),
                Clerk = HttpContext.Current.User.Identity.Name,
                EpisodeNumber = 3,
                Genre = lastOfUs.Genre,
                ImageUrl = "https://www.cnet.com/a/img/resize/d2f03181a391e8e8fd1ea721f5e775f2e94f1adc/hub/2023/01/27/91d88122-7029-4f63-ab94-15dabd31d392/nick-offerman-2.jpg?auto=webp&fit=crop&height=1200&width=1200",
                Name = "Long, Long Time",
                SeasonNumber = 1,
            });

            ds.SaveChanges();
            return true;
        }

        public ActorBaseViewModel ActorAdd(ActorAddViewModel newActor)
        {
            //Attempt to add the new actor to the database
            var addedActor = ds.Actors.Add(mapper.Map<ActorAddViewModel, Actor>(newActor));

            ds.SaveChanges();

            return addedActor == null ? null : mapper.Map<Actor, ActorBaseViewModel>(addedActor);

        }
        public IEnumerable<ActorBaseViewModel> ActorGetAll()
        {
            return mapper.Map<IEnumerable<Actor>,
                IEnumerable<ActorBaseViewModel>>
                (ds.Actors.OrderBy(m => m.Name));
        }        
        public ActorBaseViewModel ActorMediaItemAdd(ActorMediaItemAddViewModel newMediaItem)
        {
            var myActor = ds.Actors.Find(newMediaItem.ActorId);

            if(myActor == null) return null;

            var addedMediaItem = new ActorMediaItem();
            ds.ActorMediaItems.Add(addedMediaItem);

            addedMediaItem.Caption = newMediaItem.Caption;
            addedMediaItem.Actor = myActor;

            byte[] mediaBytes = new byte[newMediaItem.ContentUpload.ContentLength];
            newMediaItem.ContentUpload.InputStream.Read(mediaBytes, 0, newMediaItem.ContentUpload.ContentLength);

            addedMediaItem.Content = mediaBytes;
            addedMediaItem.ContentType = newMediaItem.ContentUpload.ContentType;

            ds.SaveChanges();

            return (addedMediaItem == null) ? null : mapper.Map<Actor, ActorBaseViewModel>(myActor);

        }
        public ActorMediaItemWithContentViewModel ActorMediaItemGetById(int id)
        {
            var myMediaItems = ds.ActorMediaItems
                .Include("Actor")
                .SingleOrDefault(a => a.Id == id);

            return(myMediaItems == null) ? null : mapper.Map<ActorMediaItem, ActorMediaItemWithContentViewModel>(myMediaItems);
        }
        public ActorWithShowInfoViewModel ActorWithShowInfoGetById(int id)
        {
            var myActor = ds.Actors
                .Include("Show")
                .Include("ActorMediaItems")
                .SingleOrDefault(i => i.Id == id);

            return (myActor == null) ? null : mapper.Map<Actor, ActorWithShowInfoViewModel>(myActor);
        }
        public EpisodeWithShowNameViewModel EpisodeAdd(EpisodeAddViewModel newEpisode)
        {
            newEpisode.Clerk = User.Name;

            int genreId = newEpisode.GenreId;
            var newGenre = ds.Genres.SingleOrDefault(g => g.Id == genreId);

            if (newGenre != null) { newEpisode.Genre = newGenre.Name; }

            var addedEp = ds.Episodes.Add(mapper.Map<EpisodeAddViewModel, Episode>(newEpisode));

            
            if(newEpisode.VideoUpload != null)
            {

            byte[] videoBytes = new byte[newEpisode.VideoUpload.ContentLength];
            newEpisode.VideoUpload.InputStream.Read(videoBytes, 0, newEpisode.VideoUpload.ContentLength);

            addedEp.Video = videoBytes;
            addedEp.VideoContentType = newEpisode.VideoUpload.ContentType;
            }
            


            ds.SaveChanges();

            return (addedEp == null) ? null : mapper.Map<Episode, EpisodeWithShowNameViewModel>(addedEp);
        }        
        public IEnumerable<EpisodeWithShowNameViewModel> EpisodesGetAll()
        {
            var e = ds.Episodes.Include("Show");

            return mapper.Map<IEnumerable<Episode>, IEnumerable<EpisodeWithShowNameViewModel>>(e.OrderBy(s => s.Show.Name).ThenBy(s => s.EpisodeNumber));

        }
        public EpisodeWithShowNameViewModel EpisodeGetOne(int id) 
        { 
            var myEpisode = ds.Episodes
                .Include("Show")
                .SingleOrDefault(e => e.Id == id);

            return (myEpisode == null) ? null : mapper.Map<Episode, EpisodeWithShowNameViewModel>(myEpisode);

        }
        public IEnumerable<GenreBaseViewModel> GenreGetAll()
        {
            return mapper.Map<IEnumerable<Genre>,
                IEnumerable<GenreBaseViewModel>>
                (ds.Genres.OrderBy(m => m.Name));
        }
        public ShowWithInfoViewModel ShowAdd(ShowAddViewModel newShow)
        {
            newShow.Coordinator = User.Name;

            int genreId = newShow.GenreId;
            var newGenre = ds.Genres.SingleOrDefault(g => g.Id == genreId);

            if(newGenre != null) { newShow.Genre = newGenre.Name; }

            int actorCount = newShow.ActorIdList.Count();

            newShow.Actors = new List<Actor>();

            for (int i = 0; i < actorCount; i++)
            {
                var currId = newShow.ActorIdList[i];
                var newActor = ds.Actors.SingleOrDefault(a => a.Id == currId);

                if (newActor != null)
                {
                    newShow.Actors.Add(newActor);
                }
            }


            var addedShow = ds.Shows.Add(mapper.Map<ShowAddViewModel, Show>(newShow));

            ds.SaveChanges();

            return (addedShow == null) ? null : mapper.Map<Show, ShowWithInfoViewModel>(addedShow);
        }
        public IEnumerable<ShowBaseViewModel> ShowGetAll()
        {
            return mapper.Map<IEnumerable<Show>,
                IEnumerable<ShowBaseViewModel>>
                (ds.Shows.OrderBy(m => m.Name));
        }
        public ShowWithInfoViewModel ShowGetOne(int id)
        {
            var myShow = ds.Shows
                .Include("Actors")
                .Include("Episodes")
                .SingleOrDefault(i => i.Id == id);

            return (myShow == null) ? null : mapper.Map<Show, 
                ShowWithInfoViewModel>(myShow);
        }
        // *** Add your methods ABOVE this line **

        #region Role Claims

        public List<string> RoleClaimGetAllStrings()
        {
            return ds.RoleClaims.OrderBy(r => r.Name).Select(r => r.Name).ToList();
        }

        #endregion Role Claims

        #region Load Data Methods

        // Add some programmatically-generated objects to the data store
        // Write a method for each entity and remember to check for existing
        // data first.  You will call this/these method(s) from a controller action.
        public bool LoadRoles()
        {
            // User name
            var user = HttpContext.Current.User.Identity.Name;
            
            // Monitor the progress
            bool done = false;

            // *** Role claims ***
            if (ds.RoleClaims.Count() == 0)
            {
                ds.RoleClaims.Add(new RoleClaim() { Name = "Administrator" });
                ds.RoleClaims.Add(new RoleClaim { Name = "Executive" });
                ds.RoleClaims.Add(new RoleClaim { Name = "Coordinator" });
                ds.RoleClaims.Add(new RoleClaim { Name = "Clerk" });
                // Add additional role claims here

                ds.SaveChanges();
                done = true;
            }

            return done;
        }

        public bool RemoveData()
        {
            try
            {


                foreach (var e in ds.Episodes)
                {
                    ds.Entry(e).State = System.Data.Entity.EntityState.Deleted;
                }
                foreach (var e in ds.Shows)
                {
                    ds.Entry(e).State = System.Data.Entity.EntityState.Deleted;
                }
                foreach (var e in ds.Actors)
                {
                    ds.Entry(e).State = System.Data.Entity.EntityState.Deleted;
                }
                foreach( var e in ds.Genres)
                {
                    ds.Entry(e).State = System.Data.Entity.EntityState.Deleted;
                }

                

                // Remove additional entities as needed.

                ds.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool RemoveDatabase()
        {
            try
            {
                return ds.Database.Delete();
            }
            catch (Exception)
            {
                return false;
            }
        }
   
        public EpisodeVideoViewModel EpisodeVideoGetById(int id)
        {
            var epVideo = ds.Episodes.Find(id);

            return (epVideo.Video
                == null) ? null : mapper.Map<Episode, EpisodeVideoViewModel>(epVideo);
        }
    }

    #endregion Load Data Methods

    #region RequestUser Class

    // This "RequestUser" class includes many convenient members that make it
    // easier work with the authenticated user and render user account info.
    // Study the properties and methods, and think about how you could use this class.

    // How to use...
    // In the Manager class, declare a new property named User:
    //    public RequestUser User { get; private set; }

    // Then in the constructor of the Manager class, initialize its value:
    //    User = new RequestUser(HttpContext.Current.User as ClaimsPrincipal);

    public class RequestUser
    {
        // Constructor, pass in the security principal
        public RequestUser(ClaimsPrincipal user)
        {
            if (HttpContext.Current.Request.IsAuthenticated)
            {
                Principal = user;

                // Extract the role claims
                RoleClaims = user.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value);

                // User name
                Name = user.Identity.Name;

                // Extract the given name(s); if null or empty, then set an initial value
                string gn = user.Claims.SingleOrDefault(c => c.Type == ClaimTypes.GivenName).Value;
                if (string.IsNullOrEmpty(gn)) { gn = "(empty given name)"; }
                GivenName = gn;

                // Extract the surname; if null or empty, then set an initial value
                string sn = user.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Surname).Value;
                if (string.IsNullOrEmpty(sn)) { sn = "(empty surname)"; }
                Surname = sn;

                IsAuthenticated = true;
                // You can change the string value in your app to match your app domain logic
                IsAdmin = user.HasClaim(ClaimTypes.Role, "Administrator") ? true : false;
            }
            else
            {
                RoleClaims = new List<string>();
                Name = "anonymous";
                GivenName = "Unauthenticated";
                Surname = "Anonymous";
                IsAuthenticated = false;
                IsAdmin = false;
            }

            // Compose the nicely-formatted full names
            NamesFirstLast = $"{GivenName} {Surname}";
            NamesLastFirst = $"{Surname}, {GivenName}";
        }

        // Public properties
        public ClaimsPrincipal Principal { get; private set; }

        public IEnumerable<string> RoleClaims { get; private set; }

        public string Name { get; set; }

        public string GivenName { get; private set; }

        public string Surname { get; private set; }

        public string NamesFirstLast { get; private set; }

        public string NamesLastFirst { get; private set; }

        public bool IsAuthenticated { get; private set; }

        public bool IsAdmin { get; private set; }

        public bool HasRoleClaim(string value)
        {
            if (!IsAuthenticated) { return false; }
            return Principal.HasClaim(ClaimTypes.Role, value) ? true : false;
        }

        public bool HasClaim(string type, string value)
        {
            if (!IsAuthenticated) { return false; }
            return Principal.HasClaim(type, value) ? true : false;
        }
    }

    #endregion RequestUser Class
}