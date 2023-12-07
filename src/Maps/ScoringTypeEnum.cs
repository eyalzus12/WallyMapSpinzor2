namespace WallyMapSpinzor2;

//This enum is merely for convenience.
//Do not use it as a type of paramater, since that could break with newer scoring types.
public enum ScoringTypeEnum
{
    //use 0 for none

    //Ghost brawl, and any of its variants, aren't actual ScoringType's
    //They're a toggleable option for GamemodeType's

    //Template
    XLTemplate = 1,
    //Timed
    TIMED,
    //Stock
    STOCK,
    //Brawlball
    BRAWLBALL,
    //Bombsketball
    BOMBSKETBALL,
    //Dodgebomb
    RICOCHET,
    //UNUSED
    HOLDTHETHING,
    //UNUSED
    CONQUEST,
    //UNUSED
    DODGEBALL,
    //Beach Brawl
    VOLLEYBALL,
    //UNUSED
    GAUNTLET,
    //UNUSED
    HOTPOTATO,
    //UNUSED
    ARCADE,
    //UNUSED
    HYDRA,
    //Snowbrawl
    SNOWBALL,
    //Strikeout
    RELAY,
    //Catch Bombs Training (training room)
    CATCHBOMBS,
    //UNUSED
    HOCKEY,
    //UNUSED
    TAUNTBRAWL,
    //UNUSED
    SIMON,
    //UNUSED
    BOUNTYHUNTER,
    //Switchcraft
    SCRAMBLE,
    //Platform King (brawl of the week)
    COLORPLATFORMS,
    //UNUSED
    POGO,
    //UNUSED
    SKEEBOMB,
    //UNUSED
    SUPERBRAWL,
    //UNUSED
    GIANT,
    //UNUSED
    PARTYMODE,
    //UNUSED
    KOTH,
    //Capture the Flag
    CTF,
    //Dodgebomb Timed (brawl of the week)
    RICOCHETTIMED,
    //Water Bomb Bash (brawl of the week)
    TIMEDWATERBOMB,
    //UNUSED
    HAUNTEDFLOORS,
    //Kung foot
    SOCCER,
    //Horde
    HORDE,
    //Buddy
    BUDDY,
    //Brawldown
    RING,
    //Bubble Tag
    TAG,
    //Morph
    SHIFT,
    //Temple Climb
    CLIMB,
    //Showdown
    SHOWDOWN,
    //Walker Attack!
    ZOMBIE,
    //Crew Battle
    CREWBATTLE,
    //Morph Timed (brawl of the week)
    SHIFTTIMED,
    //Street Brawl
    STREET_BRAWL,
    //Bounty
    BOUNTY_V2,
    //Dice & Destruction
    TABLETOP,
    //Volleybrawl
    VOLLEY_BATTLE,
    //Oops! All Boomerangs (brawl of the week)
    OOPS_ALL_BOOMERANGS,
    //Oddbrawl
    ODDBRAWL,
    //Terminus-plosions! (brawl of the week)
    BOMBMANIA,
    //Street Brawl Strikeout (brawl of the week)
    STREET_BRAWL_RELAY,
    //Bubble Tag Strikeout (brawl of the week)
    TAGRELAY
}