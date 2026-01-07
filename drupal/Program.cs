using Drupal.Domain.Interfaces;
using Drupal.Domain.Services;
using Drupal.Infrastructure.Database;
using Drupal.Infrastructure.Database.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IQuestionService, QuestionService>();
builder.Services.AddTransient<IQuestionRepository, QuestionRepository>();
builder.Services.AddTransient<IUserRepository, UserRepository>();

builder.Services.AddDbContext<DrupalDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReact",
        policy =>
        {
            policy.WithOrigins("http://localhost:3000", "http://localhost:3001")
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        });
});
var app = builder.Build();
app.UseCors("AllowReact");

List<string> questions = new List<string>
{
    "De l'ananas sur la pizza ?",
    "Chocolatine ?",
    "Du lait dans le café ?",
    "Les hot-dogs sont des sandwichs ?",
    "Céréales d'abord, puis le lait ?",
    "Le ketchup va sur les pâtes ?",
    "Le fromage dans la raclette doit couler jusqu'à la table ?",
    "Pineapple belongs on pizza ?",
    "Les toilettes : papier par dessus ou par dessous ?",
    "Gif se prononce 'jif' ?",
    "Le bout du pain s'appelle un quignon ?",
    "Il faut tremper ses frites dans le milkshake ?",
    "On met du beurre dans les pâtes ?",
    "La tomate est un fruit ?",
    "Die Hard est un film de Noël ?",
    "On peut porter des chaussettes avec des sandales ?",
    "Le mojito doit être écrasé au pilon ?",
    "Mettre la mayonnaise au frigo ?",
    "Le Nutella doit être conservé au frais ?",
    "Plier ou froisser le papier toilette ?",
    "Le siège des toilettes doit rester baissé ?",
    "Manger les frites avec les doigts ?",
    "Le burger se mange à l'envers ?",
    "Les crocs sont des vraies chaussures ?",
    "Il faut rincer la vaisselle avant le lave-vaisselle ?",
    "Le fromage dans la fondue savoyarde doit avoir de l'ail ?",
    "On dit pain au chocolat ?",
    "Les bonbons Haribo c'est pour les enfants ?",
    "Mettre du sucre dans la sauce tomate ?",
    "Star Wars épisode 1 est un bon film ?",
    "Passer l'aspirateur tous les jours ?",
    "Les crêpes salées avant les sucrées ?",
    "Rouler le tube de dentifrice depuis le bas ?",
    "Mettre du sel dans l'eau des pâtes ?",
    "Le tiramisu doit avoir du rhum ?",
    "Les sushis se mangent avec les doigts ?",
    "On laisse la peau de la pomme ?",
    "Couper les spaghettis avant cuisson ?",
    "Le fromage sur la tartiflette doit être bien gratiné ?",
    "Mettre de la crème fraîche dans la carbonara ?",
    "Les baskets blanches avec un costume ?",
    "Laver son jean après chaque utilisation ?",
    "Le thé glacé doit avoir du sucre ?",
    "Manger la pizza avec un couteau et une fourchette ?",
    "Le cappuccino après 11h du matin ?",
    "Mettre du lait dans le thé ?",
    "Le burger doit déborder ?",
    "Manger les nuggets avec du ketchup ?",
    "Le guacamole doit avoir des morceaux ?",
    "Porter un jean avec un t-shirt rentré dedans ?",
    "Le champagne uniquement pour les occasions spéciales ?"
};

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<DrupalDbContext>();
    db.Database.Migrate();
    var questionRepository = scope.ServiceProvider.GetRequiredService<IQuestionRepository>();

    foreach (var questionText in questions)
    {
        await questionRepository.Create(questionText);
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
