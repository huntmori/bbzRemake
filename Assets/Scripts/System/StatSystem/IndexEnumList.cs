
public class IndexEnumList
{
    public enum Stat: int
    { attack, move, deffence, skill };
    public enum StatNames : int
    {
        attackDamage, attackSpeed, attackRange, attackRadius,
        moveSpeed, rotationSpeed, detectRange,
        deffencePoint, currentHealth, maxHealth, healthRegeneration, damageReduce,
        totalSkillCount, skillPoint, currentSkillResource, maxSkillResource, skillResourceRegeneration
    };
    /*
     *	1=attackDamage,			2=attackSpeed,		3=attackRange,			4=attackRadius,
     *	5=moveSpeed,			6=rotationSpeed,	7=dectectRange,			8=deffencePoint, 
	 *	9=currentHealth,		10=maxHealth,		11=healthRegeneration,	12=damageReduce,
     *	13=totalSkillCount, 	14=startSkillPoint,	15=currentSkillResoure, 16=maxSkillResource, 
	 *	17=skillResourceRegeneration
     */
    public enum SkillId : int
    { doubleSlash };

    public enum PointBarNames : int
    { facePicture, health, mana, exp };
    public enum RectParameter : int
    { left, top, width, height, max, curr};
    public enum BarValues : int
    { max, current};

    public enum DamageType
    { PHYSICAL, FIRE, ICE, LIGHTNING, PHANTOM }
}