﻿using Characters.Abilities;
using Characters.Repositories;
using Players;

namespace Characters.Types
{
    public class Manticore : ACharacter
    {
        public override void Initialize(PlayerManager playerManager, CharacterDto character = null)
        {
            // Arena character
            if (character == null)
            {
                InitializeSpeed();
            }
            // RLR Character
            else
            {
                Character = character;
                InitializeFromDto();
                Ability1 = new GravityField(this, playerManager);
                Ability2 = new Freeze(this, playerManager);
            }
        }
    }
}
