
namespace Valkyrie.GL
{
    /*==================================================
     * 
     * These enums control interactions and motion
     * in the Game Logic layer, and which sprite 
     * represents the character in the graphics layer.
     * 
     * ===============================================*/

    public enum Direction { left, right };

    //------------------------------------------

    public enum Status
    {
        standing,
        crouching,
        falling,
        attack
    };

    //==================================================

    /*------------------------------
     * 
     *  Enums pertaining to damage
     * 
     * ----------------------------*/

    public enum DamageType
    {
        melee,
        piercing
    };
}