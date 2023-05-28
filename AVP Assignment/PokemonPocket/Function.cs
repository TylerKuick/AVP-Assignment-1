using System;
using System.Collections.Generic;
using System.Linq;
using PokemonPocket;

namespace Function {
    public class Funct {
        public Boolean checkMasterName(string name, List<PokemonMaster> pokemonMasters) {
            Boolean valid = false;
            foreach (var i in pokemonMasters) {
                if(name.Equals(i.Name.ToLower())) {
                    valid = true;
                    return valid;
                }
            }
            return valid;
        }

        public Boolean checkName(string name, List<Pokemon> db) {
            Boolean valid = false;
            foreach (var i in db) {
                if(name.Equals(i.Name.ToLower())) {
                    valid = true;
                    return valid;
                }
            }
            return valid;
        }


        // Checks if there are any pokemon that can be evolved
        public Boolean checkDbEvolve(List<Pokemon> db, List<PokemonMaster> pokemonMasters) {
            Boolean canEvolve = false;
            foreach(var j in db) {
                foreach (var i in pokemonMasters) {
                    if (j.Name.ToLower().Equals(i.Name.ToLower())) {
                        if (j.getCount() >= i.NoToEvolve) {
                            canEvolve = true;
                        }
                    }
                }
            }
            return canEvolve;
        }


        // Checks a single pokemon can be evolved
        public Boolean checkEvolve(string name, List<Pokemon> db, List<PokemonMaster> pokemonMasters) {
            Boolean canEvolve = false;
            try {
                var pokemon = db.Where(p=> p.Name.ToLower() == name.ToLower()).First();
                int no_to_evolve = pokemonMasters.Where(p=> p.Name.ToLower() == name.ToLower()).Select(p=> p.NoToEvolve).First();
                if (pokemon.getCount() >= no_to_evolve) {
                    canEvolve =true;
                    return canEvolve;
                }
                return canEvolve;
            }
            catch (System.InvalidOperationException) {
                return canEvolve;
            }
        }

        // Evolve Pokemon
        public void Evolve(string name, List<Pokemon> db, List<PokemonMaster> pokeM) {
            var query = db.Where(p=>p.Name.ToLower() == name).ToList();     // Find all pokemon with the input name to evolve, Desc HP
            var evolveto = pokeM.Where(p=>p.Name.ToLower() == name).Select(p=>p.EvolveTo).First(); 
            // Take highest HP pokemon to evolve
            var evolve_pokemon = query[0];
            evolve_pokemon.Name = evolveto;
            evolve_pokemon.HP = 100;
            evolve_pokemon.Exp = 0;
            evolve_pokemon.Org_HP = 100;
            evolve_pokemon.decCount(); // Decrease Pokemon count
        }

        // Heal Pokemon
        public void Heal(Pokemon pokemon) {
            pokemon.HP = pokemon.Org_HP;
        }
    }
}
