using System;

public class Pokemon
{
    public string Nome { get; set; }
    public string Tipo { get; set; }
    public int Vida { get; protected set; } 
    
    public int Ataque { get; protected set; }
    
    
    public int Defesa { get; protected set; } 
    
    protected Random random = new Random();

    public Pokemon(string nome, string tipo, int vida = 50, int ataque = 5, int defesa = 2)
    {
        this.Nome = nome;
        this.Tipo = tipo;
        this.Vida = vida;
        this.Ataque = ataque;
        this.Defesa = defesa;
    }

    public virtual void ExibirStatus()
    {
        Console.WriteLine($"{Nome} ({Tipo}) | Vida: {Vida} | Ataque: {Ataque} | Defesa: {Defesa}");
    }

    public virtual void Atacar(Pokemon alvo)
    {
        int dano = CalcularDano(alvo);
        
        if (dano < 1) dano = 1;
        
        alvo.Vida -= dano;
        if (alvo.Vida < 0) alvo.Vida = 0;
        
        Console.WriteLine($"{Nome} atacou {alvo.Nome} e causou {dano} de dano.");
        Console.WriteLine($"{alvo.Nome} agora está com {alvo.Vida} de vida.");
    }

    protected virtual int CalcularDano(Pokemon alvo)
    {
        return Ataque - alvo.Defesa;
    }


    protected void RecuperarVida(int quantidade)
    {
        Vida += quantidade;
        Console.WriteLine($"{Nome} recuperou {quantidade} de vida! Agora tem {Vida} de vida.");
    }

    public bool EstaVivo()
    {
        return Vida > 0;
    }
}


public class PokemonFogo : Pokemon
{
    public PokemonFogo(string nome, int vida = 50, int ataque = 5, int defesa = 2) 
        : base(nome, "Fogo", vida, ataque, defesa) { }

    protected override int CalcularDano(Pokemon alvo)
    {
        int dano = base.CalcularDano(alvo);
      
      
        if (alvo.Tipo == "Grama")
        {
            dano += 3;
            Console.WriteLine($"{Nome} tem vantagem sobre {alvo.Nome}!");
        }
        
        dano += 2;
        return dano;
    }
}

public class PokemonAgua : Pokemon
{
    public PokemonAgua(string nome, int vida = 50, int ataque = 5, int defesa = 2) 
        : base(nome, "Água", vida, ataque, defesa) { }

    protected override int CalcularDano(Pokemon alvo)
    {
        int dano = base.CalcularDano(alvo);
        
        if (alvo.Tipo == "Fogo")
        {
            dano += 3;
            Console.WriteLine($"{Nome} tem vantagem sobre {alvo.Nome}!");
        }
        
        return dano;
    }

    public override void Atacar(Pokemon alvo)
    {
        base.Atacar(alvo);
        
        RecuperarVida(2);
    }
}

public class PokemonGrama : Pokemon
{
    public PokemonGrama(string nome, int vida = 50, int ataque = 5, int defesa = 2) 
        : base(nome, "Grama", vida, ataque, defesa) { }

    protected override int CalcularDano(Pokemon alvo)
    {
        int dano = base.CalcularDano(alvo);
        
        
        if (alvo.Tipo == "Água")
        {
            dano += 3;
            Console.WriteLine($"{Nome} tem vantagem sobre {alvo.Nome}!");
        }
        
      
        if (random.Next(100) < 20)
        {
            int danoCritical = dano * 2;
            Console.WriteLine($"{Nome} acertou um ATAQUE CRÍTICO!");
            return danoCritical;
        }
        
        return dano;
    }
}

//lista ligada do pokemaster kkkkkkkkkkkkkkk
public class NoPokemon
{
    public Pokemon Pokemon { get; set; }
    public NoPokemon Proximo { get; set; }

    public NoPokemon(Pokemon pokemon)
    {
        Pokemon = pokemon;
        Proximo = null;
    }
}

public class Treinador
{
    public string Nome { get; set; }
    private NoPokemon primeiro;
    private int quantidade;

    public Treinador(string nome)
    {
        Nome = nome;
        primeiro = null;
        quantidade = 0;
    }

    public void AdicionarPokemon(Pokemon p)
    {
        NoPokemon novoNo = new NoPokemon(p);
        
        if (primeiro == null)
        {
            primeiro = novoNo;
        }
        else
        {
            NoPokemon atual = primeiro;
            while (atual.Proximo != null)
            {
                atual = atual.Proximo;
            }
            atual.Proximo = novoNo;
        }
        quantidade++;
        Console.WriteLine($"{Nome} adicionou {p.Nome} à sua equipe!");
    }

    public void ListarPokemons()
    {
        Console.WriteLine($"\n--- Pokémons de {Nome} ---");
        if (primeiro == null)
        {
            Console.WriteLine("Nenhum Pokémon na equipe.");
            return;
        }

        NoPokemon atual = primeiro;
        int indice = 1;
        while (atual != null)
        {
            Console.Write($"{indice}. ");
            atual.Pokemon.ExibirStatus();
            atual = atual.Proximo;
            indice++;
        }
    }

    public Pokemon EscolherPokemon(int indice)
    {
        if (indice < 1 || indice > quantidade)
        {
            Console.WriteLine("Índice inválido!");
            return null;
        }

        NoPokemon atual = primeiro;
        for (int i = 1; i < indice; i++)
        {
            atual = atual.Proximo;
        }
        
        Console.WriteLine($"\n{Nome} escolheu {atual.Pokemon.Nome}!");
        return atual.Pokemon;
    }

    public int QuantidadePokemons()
    {
        return quantidade;
    }
}

class Program
{
    static void Main(string[] args)
    {
        Treinador Thiagao = new Treinador("Thiagao");
        Treinador GuiReyzao = new Treinador("GuiReyzao");
        Treinador Matheuzao = new Treinador("Matheuzao");

       
        Thiagao.AdicionarPokemon(new PokemonFogo("fogudo", 45, 8, 3));
        Thiagao.AdicionarPokemon(new PokemonGrama("plantudo", 55, 6, 4));
        Thiagao.AdicionarPokemon(new PokemonAgua("aguado", 50, 7, 3));

        
        GuiReyzao.AdicionarPokemon(new PokemonAgua("tigrinho", 52, 9, 4));
        GuiReyzao.AdicionarPokemon(new PokemonAgua("cavalinho", 48, 6, 3));
        GuiReyzao.AdicionarPokemon(new PokemonGrama("cassinoline", 55, 5, 5));

        
        Matheuzao.AdicionarPokemon(new PokemonFogo("burro", 60, 7, 5));
        Matheuzao.AdicionarPokemon(new PokemonAgua("kleklek", 50, 6, 4));

        
        Thiagao.ListarPokemons();
        GuiReyzao.ListarPokemons();
        Matheuzao.ListarPokemons();


        Console.WriteLine("BATALHA: THIAGAO vs GUIREYZAO");
        

        Pokemon pokemonThiagao = Thiagao.EscolherPokemon(1); 
        Pokemon pokemonGuiReyzao = GuiReyzao.EscolherPokemon(1); 


        int turno = 1;
        bool batalhaAtiva = true;

        while (batalhaAtiva)
        {
            Console.WriteLine($"\n--- TURNO {turno} ---");
            
            
            if (pokemonThiagao.EstaVivo() && pokemonGuiReyzao.EstaVivo())
            {
                pokemonThiagao.Atacar(pokemonGuiReyzao);
                
                if (!pokemonGuiReyzao.EstaVivo())
                {
                    Console.WriteLine($"\n{pokemonGuiReyzao.Nome} foi derrotado!");
                    Console.WriteLine($"{pokemonThiagao.Nome} venceu a batalha!");
                    break;
                }
            }

            if (pokemonThiagao.EstaVivo() && pokemonGuiReyzao.EstaVivo())
            {
                pokemonGuiReyzao.Atacar(pokemonThiagao);
                
                if (!pokemonThiagao.EstaVivo())
                {
                    Console.WriteLine($"\n{pokemonThiagao.Nome} foi derrotado!");
                    Console.WriteLine($"{pokemonGuiReyzao.Nome} venceu a batalha!");
                    break;
                }
            }

            turno++;
            
            if (turno > 20)
            {
                Console.WriteLine("\nBatalha empatou após muitos turnos!");
                break;
            }
        }

       
        Console.WriteLine("BATALHA ESPECIAL: MATHEUZAO vs THIAGAO");


        Pokemon pokemonMatheuzao = Matheuzao.EscolherPokemon(1);
        Pokemon pokemonThiagao2 = Thiagao.EscolherPokemon(2); 

        turno = 1;
        batalhaAtiva = true;

        while (batalhaAtiva)
        {
            Console.WriteLine($"\n--- TURNO {turno} ---");
            
            if (pokemonThiagao2.EstaVivo() && pokemonMatheuzao.EstaVivo())
            {
                pokemonThiagao2.Atacar(pokemonMatheuzao);
                
                if (!pokemonMatheuzao.EstaVivo())
                {
                    Console.WriteLine($"\n{pokemonMatheuzao.Nome} foi derrotado!");
                    Console.WriteLine($"{pokemonThiagao2.Nome} venceu a batalha!");
                    break;
                }
            }

            if (pokemonThiagao2.EstaVivo() && pokemonMatheuzao.EstaVivo())
            {
                pokemonMatheuzao.Atacar(pokemonThiagao2);
                
                if (!pokemonThiagao2.EstaVivo())
                {
                    Console.WriteLine($"\n{pokemonThiagao2.Nome} foi derrotado!");
                    Console.WriteLine($"{pokemonMatheuzao.Nome} venceu a batalha!");
                    break;
                }
            }

            turno++;
        }

        Console.WriteLine("\n=== FIM DA SIMULAÇÃO ===");
    }
}