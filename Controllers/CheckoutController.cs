using Microsoft.AspNetCore.Mvc;
using Team2_DotNetCA.Data;
using Team2_DotNetCA.Controllers;

namespace Team2_DotNetCA.Controllers
{
    public class CheckoutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CheckoutHandler(int uId)
        {
            
            if (uId == 0) 
            {
                // GUEST USERS

                // STEP 1: redirect to login page for user to log in
                return RedirectToAction("Index", "Login"); 

                // STEP 2: after login, add cart information from cookies to database --> using /Login/CookiesToCartDB

                //FOR TESTING if "CookiesToCartDB" method works:
                //return RedirectToAction("CookiesToCartDB", "Login", new { userId = 2 });
            }
            else 
            {
                // FOR USERS WHO ALREADY LOGGED IN
                
                // remove items from Cart and add to purchase history
                bool checkoutCart = CartData.TrfFrCartToPurchHist(uId);

                // check if successful 
                if (checkoutCart)
                {
                    return RedirectToAction("Index", "Purchase"); //go to view Purchase History
                }
                else
                {
                    return RedirectToAction("ViewCart", "Cart"); // if checkout unsuccessful, go back to View Cart
                }

                /* TEST CASE. 
                 * Created these 2 Test views to see if Checkout Use Case for Logged-In users work
                 
                if (checkoutCart)
                {
                    return View("~/Views/Checkout/CheckoutSuccess.cshtml");
                }
                else
                {
                    return View("~/Views/Checkout/CheckoutFailiure.cshtml");
                 */
            }
        }
    }
}
