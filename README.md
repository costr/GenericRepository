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

Your actual repository of the article:

var articleRepo = new RepositoryInt < Article >(new ApplicationDbContext());

so when you call something like:

  articleRepo.GetById(1);
  
this is saying get me this article, and no child "defaults" or any other ones

  articleRepo.GetById(1, true);
  
says get me the article and all defaulted child properties specified in the "default includes"

  articleRepo.GetById(1, true, include=>include.SomethingElseToo);
  
says get me the article, the defaults AND this extra thing too.

