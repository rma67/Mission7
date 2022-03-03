using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Mission7.Infrastrucure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Mission7.Models
{
    public class SessionBasket : Basket
    {
        public static Basket GetBasket (IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;

            SessionBasket basket = session?.GetJson<SessionBasket>("Basket") ?? new SessionBasket();

            basket.Session = session;

            return basket;
        }

        [JsonIgnore]
        public ISession Session { get; set; }

        public override void AddItem(Book bok, int qty, double prc)
        {
            base.AddItem(bok, qty, prc);
            Session.SetJson("Basket", this);
        }

        public override void RemoveItem(Book bok)
        {
            base.RemoveItem(bok);
            Session.SetJson("Basket", this);
        }

        public override void ClearItems()
        {
            base.ClearItems();
            Session.Remove("Basket");
        }
    }
}