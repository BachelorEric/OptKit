using OptKit.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace OptKit.UnitTest
{
    public class User : Entity<double>
    {
        public virtual string Name { get; set; }

        public virtual double Age { get; set; }
    }

    public class Order : Entity<double>
    {
        public virtual DateTime Date { get; set; }

        public virtual double UserId { get; set; }

        public virtual User User { get; set; }

        public virtual IList<OrderItem> OrderList { get; set; }
    }

    public class OrderItem : Entity<double>
    {
        public virtual double OrderId { get; set; }
        public virtual Order Order { get; set; }
    }
}
