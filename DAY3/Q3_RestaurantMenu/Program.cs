using System;
using System.Collections.Generic;
using System.Linq;

class MenuItem
{
    public string Name { get; set; }
    public string CourseCategory { get; set; } // Starter, Main Course, Dessert
    public double Price { get; set; }
}

class Menu
{
    public string Name { get; set; }
    public bool IsSpecial { get; set; }
    public List<MenuItem> Items { get; set; } = new List<MenuItem>();

    public double GetDiscountedPrice(MenuItem item)
    {
        if (IsSpecial)
            return item.Price * 0.70; // 30% discount
        return item.Price;
    }
}

class Restaurant
{
    public string Name { get; set; }
    public string Location { get; set; }
    public List<Menu> Menus { get; set; } = new List<Menu>();

    public int TotalMenuItems()
    {
        return Menus.Sum(m => m.Items.Count);
    }

    public List<MenuItem> GetItemsByCourse(string courseCategory)
    {
        return Menus.SelectMany(m => m.Items)
                    .Where(i => i.CourseCategory.Equals(courseCategory, StringComparison.OrdinalIgnoreCase))
                    .ToList();
    }

    public List<(Menu menu, MenuItem item, double discountedPrice)> GetSpecialMenuItems()
    {
        var result = new List<(Menu, MenuItem, double)>();
        foreach (var menu in Menus.Where(m => m.IsSpecial))
            foreach (var item in menu.Items)
                result.Add((menu, item, menu.GetDiscountedPrice(item)));
        return result;
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("=== Q3: Restaurant Menu ===\n");

        var restaurant = new Restaurant { Name = "Spice Garden", Location = "Chennai" };

        var regularMenu = new Menu { Name = "Regular Menu", IsSpecial = false };
        regularMenu.Items.Add(new MenuItem { Name = "Soup", CourseCategory = "Starter", Price = 150 });
        regularMenu.Items.Add(new MenuItem { Name = "Grilled Chicken", CourseCategory = "Main Course", Price = 350 });
        regularMenu.Items.Add(new MenuItem { Name = "Ice Cream", CourseCategory = "Dessert", Price = 120 });
        regularMenu.Items.Add(new MenuItem { Name = "Spring Rolls", CourseCategory = "Starter", Price = 180 });

        var specialMenu = new Menu { Name = "Special Menu", IsSpecial = true };
        specialMenu.Items.Add(new MenuItem { Name = "Lobster Bisque", CourseCategory = "Starter", Price = 400 });
        specialMenu.Items.Add(new MenuItem { Name = "Grilled Salmon", CourseCategory = "Main Course", Price = 800 });
        specialMenu.Items.Add(new MenuItem { Name = "Chocolate Lava Cake", CourseCategory = "Dessert", Price = 250 });
        specialMenu.Items.Add(new MenuItem { Name = "Steak", CourseCategory = "Main Course", Price = 950 });

        restaurant.Menus.Add(regularMenu);
        restaurant.Menus.Add(specialMenu);

        Console.WriteLine($"Total menu items: {restaurant.TotalMenuItems()}\n");

        Console.WriteLine("Items in 'Starter' category:");
        foreach (var item in restaurant.GetItemsByCourse("Starter"))
            Console.WriteLine($"  {item.Name} - Rs.{item.Price}");

        Console.WriteLine("\nSpecial menu items (30% discount):");
        foreach (var (menu, item, price) in restaurant.GetSpecialMenuItems())
            Console.WriteLine($"  {item.Name} | Original: Rs.{item.Price} | After Discount: Rs.{price:F2}");
    }
}
