using System;
using System.Collections.Generic;

namespace PokemonPocket{
    public class PokemonMaster{
        public string Name {get;set;}
        public int NoToEvolve {get; set;}
        public  string EvolveTo {get; set;}

        public PokemonMaster(string name, int noToEvolve, string evolveTo){
            this.Name = name;
            this.NoToEvolve = noToEvolve;
            this.EvolveTo = evolveTo;
        }
    }

    public class Pokemon {
        // Base Requirements for Pokemon Class
        private static int Count = 0; 
        public string Name {get; set;}
        public int HP {get; set;}
        public int Exp {get; set;}
        public string Skill {get; set;}
        public int Skill_Dmg {get; set;}
        
        // Additional Attributes
        public int Org_HP {get; set;}

        // Constructors
        public Pokemon() {}
        public Pokemon(string name, int hp, int exp) {
            this.Name = name;
            this.HP = hp;
            this.Exp = exp;
            this.Skill = "";
            this.Skill_Dmg = 0;
        }

        // Methods
        public void calculateDamage(int opp_dmg) {
            int multiplier = 0;
            var ref_pikachu = new Pikachu();
            var ref_eevee = new Eevee();
            var ref_charmander = new Charmander();
            if (ref_pikachu.Skill_Dmg == opp_dmg) {
                multiplier = 1;
            }
            else if (ref_eevee.Skill_Dmg == opp_dmg) {
                multiplier = 2;
            }
            else if (ref_charmander.Skill_Dmg == opp_dmg) {
                multiplier = 3;
            }
            this.HP -= (multiplier * opp_dmg);

            // If hp goes below 0, pokemon has fainted, set hp to 0
            if (this.HP < 0) {
                this.HP = 0;
            }
        }
        public virtual int getCount() {
            return Count;
        }

        public virtual void decCount() {
            Count--;
        }
    } 
    
    public class Pikachu : Pokemon {
        private static int Count = 0; 
        public Pikachu() {}
        public Pikachu(string name, int hp, int exp) : base(name, hp, exp) {
            this.Name = name;
            this.HP = hp;
            this.Exp = exp;
            this.Skill = "Lightning Bolt";
            this.Skill_Dmg = 25;
            this.Org_HP = hp;
            Count++;
        }  

        public override int getCount() {
            return Count;
        }

        public override void decCount() {
            Count--;
        }
        
    }
    
    public class Eevee : Pokemon {
        private static int Count = 0; 
        public Eevee() {}
        public Eevee(string name, int hp, int exp) : base(name, hp, exp) {
            this.Name = name;
            this.HP = hp;
            this.Exp = exp;
            this.Skill = "Run Away";
            this.Skill_Dmg = 20;
            this.Org_HP = hp;
            Count++;
        }  

        public override int getCount() {
            return Count;
        }

        public override void decCount() {
            Count--;
        }
        
    }

    public class Charmander : Pokemon {
        private static int Count = 0; 
        public Charmander() {}
        public Charmander(string name, int hp, int exp) : base(name, hp, exp) {
            this.Name = name;
            this.HP = hp;
            this.Exp = exp;
            this.Skill = "Solar Power";
            this.Skill_Dmg = 15;
            this.Org_HP = hp;
            Count++;
        }  

        public override int getCount() {
            return Count;
        }

        public override void decCount() {
            Count--;
        }
        
    }
  
}
