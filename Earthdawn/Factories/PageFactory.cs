using Earthdawn.Data;
using Earthdawn.ViewModels;
using System;

namespace Earthdawn.Factories;

public class PageFactory
{
   private readonly Func<ApplicationPageNames, PageViewModel> pageFactory;
   
   public PageFactory(Func<ApplicationPageNames, PageViewModel> factory)
   {
      pageFactory = factory;
   }

   public PageViewModel GetPageViewModel(ApplicationPageNames pageName) => pageFactory.Invoke(pageName);
}