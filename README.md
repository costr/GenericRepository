GenericRepository
=================
Repository for Int, String and Long's as Id's



Each of the three types have a few requirements

  1) That you inherit the ISavable type of your object
  2) The Id field is named Id
  3) You have lazy loading OFF in the DbContext
  4) After inheriting the ISavable you'll need to set a group of "DefaultIncludes" which will be "Always included" for those calls you send to the DB when the call is made from each repository.
  
  Example:
  
  public class Article : ISaveableInt{
      public int Id{get;set;}
      [NotMapped]
      public Expression < Func< Article, Object > > [] DefaultIncludes{get{
          //return //your set of includes;
        }
      }
      //other properties
  }
  
  if this type of implementation doesn't work. you can always just have a a static class with the same expression function and pass those values into the repo functions

Your actual repository of the article:

var articleRepo = new RepositoryInt < Article >(new ApplicationDbContext());

so when you call something like:

  articleRepo.GetById(1);
  
This repository wouldn't have been possible without the initial explanation and understanding from Adam Costenbader.  The idea of using Generics, he came up with Generics examples I had orignally used. His version assumed you were lazy loading your child entities.  I added the ability to offer both lazy loading or specific entities.
@costr is his Github profile
