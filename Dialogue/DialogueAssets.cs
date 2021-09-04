using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Contains all the dialogue expressions for all the characters
public class DialogueAssets : Singleton<DialogueAssets>
{
    public Sprite narratorDialogueBox;
    public Sprite kyDialogueBox;
    public Sprite nikkoDialogueBox;
    public Sprite simonDialogueBox;
    public Sprite novaDialogueBox;
    public Sprite npcDialogueBox;
    #region KySprites
    public Sprite kySpriteDefault;
    public Sprite kySpriteSleepy;
    public Sprite kySpriteSleepy2;
    public Sprite kySpriteSleepy3;
    public Sprite kySpriteWakeSurprise;
    public Sprite kySpriteWakeWorry;
    public Sprite kySpriteWakeScared;
    public Sprite kySpriteWakeFear;
    public Sprite kySpriteWakeFear1;
    public Sprite kySpriteWakeFear2;
    public Sprite kySpriteWakeDetermined;
    public Sprite kySpriteScared1;
    public Sprite kySpriteScared3;
    public Sprite kySpriteFear1;
    public Sprite kySpriteFear2;
    public Sprite kySpriteDefaultScaredAnxious2;
    public Sprite kySpriteDefaultScaredAnxious;
    public Sprite kySpriteAnxious;
    public Sprite kySpriteAnxious2;
    public Sprite kySpriteAnxious3;
    public Sprite kySpriteFear3;
    public Sprite kySpriteLosingSanity;
    public Sprite kySpritePanic;
    public Sprite kySpriteScared2;
    public Sprite kySpriteScared4;
    public Sprite kySpriteDefaultDetermined;
    public Sprite kySpriteDefaultDetermined2;
    public Sprite kySpriteDefaultDetermined3;
    public Sprite kySpriteDefaultDetermined4;
    public Sprite kySpriteDefaultEmbarrased;
    public Sprite kySpriteDefaultEmbarrased2;
    public Sprite kySpriteDefaultHappy;
    public Sprite kySpriteDefaultHappy2;
    public Sprite kySpriteDefaultHappy3;
    public Sprite kySpriteDefaultLaughing;
    public Sprite kySpriteDefaultSheepish;
    public Sprite kySpriteDefaultSurprised;
    public Sprite kySpriteDefaultSurprised2;
    public Sprite kySpriteDefaultWhiteLie;
    public Sprite kySpriteDefaultWorry;
    public Sprite kySpriteDefault2Worry;
    public Sprite kySpriteAnnoyed;
    public Sprite kySpriteAnxiousNoBag;
    public Sprite kySpriteAnxious2NoBag;
    public Sprite kySpriteAnxious3NoBag;
    public Sprite kySpriteDeadpan;
    public Sprite kySpriteExasperated;
    public Sprite kySpriteFear1NoBag;
    public Sprite kySpriteFear3NoBag;
    public Sprite kySpriteScared1NoBag;
    public Sprite kySpriteScared2NoBag;
    public Sprite kySpriteScared3NoBag;
    public Sprite kySpriteWakeConfused;
    public Sprite kySpriteWakeHappy;
    public Sprite kySpriteWakeSheepish;
    public Sprite kySpriteDefaultNoBag;
    public Sprite kySpriteDefaultDetermined4NoBag;
    public Sprite kySpriteDefaultEmbarrasedNoBag;
    public Sprite kySpriteDefaultEmbarrased2NoBag;
    public Sprite kySpriteDefaultHappyNoBag;
    public Sprite kySpriteDefaultHappy3NoBag;
    public Sprite kySpriteDefaultLaughingNoBag;
    public Sprite kySpriteDefaultScaredAnxiousNoBag;
    public Sprite kySpriteDefaultScaredAnxious2NoBag;
    public Sprite kySpriteDefaultSheepishNoBag;
    public Sprite kySpriteDefaultSurprisedNoBag;

    #endregion
    #region NikkoSprites
    public Sprite nikkoSpriteAnnoyed;
    public Sprite nikkoSpriteAnnoyed2;
    public Sprite nikkoSpriteDefault;
    public Sprite nikkoSpriteDetermined;
    public Sprite nikkoSpriteDetermined2;
    public Sprite nikkoSpriteEmbarrassed;
    public Sprite nikkoSpriteGrin;
    public Sprite nikkoSpriteHappy;
    public Sprite nikkoSpriteHappy2;
    public Sprite nikkoSpritePanic;
    public Sprite nikkoSpritePanic2;
    public Sprite nikkoSpriteRelieved;
    public Sprite nikkoSpriteRelieved2;
    public Sprite nikkoSpriteSmirk1;
    public Sprite nikkoSpriteSmirk2;
    public Sprite nikkoSpriteSurprised;
    public Sprite nikkoSpriteSurprised2;
    public Sprite nikkoSpriteWave;
    public Sprite nikkoSpriteWorried1;
    public Sprite nikkoSpriteWorried2;
    public Sprite nikkoSpriteWorried3;
    public Sprite nikkoSpriteWorried4;
    public Sprite nikkoSpriteWorried5;
    public Sprite nikkoSpriteWorried6;    
    #endregion
    #region SimonSprites
    public Sprite simonSpriteComfort;
    public Sprite simonSpriteContent;
    public Sprite simonSpriteDefault2;
    public Sprite simonSpriteDefault;
    public Sprite simonSpriteEmbarrassed;
    public Sprite simonSpriteEmbarrassed2;
    public Sprite simonSpriteExasperated;
    public Sprite simonSpriteGuilt;
    public Sprite simonSpriteHappy;
    public Sprite simonSpritePanic;
    public Sprite simonSpritePanic2;
    public Sprite simonSpriteRelieved;
    public Sprite simonSpriteSurprised;
    public Sprite simonSpriteSurprised2;
    public Sprite simonSpriteWorried;
    public Sprite simonSpriteWorried2;
    public Sprite simonSpriteWorried3;
    public Sprite simonSpriteWorried4;
    public Sprite simonSpriteWorried5;
    public Sprite simonSpriteWorried6;
    public Sprite simonSpriteWorried7;    
    #endregion
    #region Nova
    public Sprite novaSpriteComfort;
    public Sprite novaSpriteDeadpan;
    public Sprite novaSpriteDetermined;
    public Sprite novaSpriteFear;
    public Sprite novaSpriteMad;
    public Sprite novaSpriteNervous;
    public Sprite novaSpritePanic;
    public Sprite novaSpriteSmile;
    public Sprite novaSpriteTaunt;
    public Sprite novaSpriteWorried;
    public Sprite novaSpriteDefault;
    #endregion
    #region Ayana
    public Sprite ayanaSpriteDefault;
    public Sprite ayanaSpriteHappy;
    public Sprite ayanaSpriteConfused;
    public Sprite ayanaSpriteShock;
    public Sprite ayanaSpriteFear;
    public Sprite ayanaSpriteAnxiousLookAway;
    public Sprite ayanaSpriteMad;
    public Sprite ayanaSpriteDepressed;
    public Sprite ayanaSpriteTearingUp;
    public Sprite ayanaSpriteCrying;
    public Sprite ayanaSpriteShockTearingUp;
    public Sprite ayanaSpriteTearySmile;
    public Sprite ayanaSpriteSheepish;
    public Sprite ayanaSpriteWorriedLaugh;
    public Sprite ayanaSpriteMadTears;
    
    #endregion

    //Given a sprite name, returns that sprite
    public Sprite ReturnSprite(string spriteName){
        switch(spriteName){
            case "kySpriteDefault":
                return kySpriteDefault;
            case "kySpriteSleepy":
                return kySpriteSleepy;
            case "kySpriteSleepy2":
                return kySpriteSleepy2;
            case "kySpriteSleepy3":
                return kySpriteSleepy3;
            case "kySpriteWakeSurprise":
                return kySpriteWakeSurprise;
            case "kySpriteWakeWorry":
                return kySpriteWakeWorry;
            case "kySpriteWakeScared":
                return kySpriteWakeScared;
            case "kySpriteWakeFear":
                return kySpriteWakeFear;
            case "kySpriteWakeFear1":
                return kySpriteWakeFear1;
            case "kySpriteWakeFear2":
                return kySpriteWakeFear2;
            case "kySpriteWakeDetermined":
                return kySpriteWakeDetermined;
            case "kySpriteScared1":
                return kySpriteScared1;
            case "kySpriteScared2":
                return kySpriteScared2;
            case "kySpriteScared3":
                return kySpriteScared3;
            case "kySpriteScared4":
                return kySpriteScared4;
            case "kySpriteFear1":
                return kySpriteFear1;
            case "kySpriteFear2":
                return kySpriteFear2;
            case "kySpriteFear3":
                return kySpriteFear3;
            case "kySpriteDefaultScaredAnxious":
                return kySpriteDefaultScaredAnxious;
            case "kySpriteDefaultScaredAnxious2":
                return kySpriteDefaultScaredAnxious2;
            case "kySpriteAnxious":
                return kySpriteAnxious;
            case "kySpriteAnxious2":
                return kySpriteAnxious2;
            case "kySpriteAnxious3":
                return kySpriteAnxious3;
            case "kySpriteLosingSanity":
                return kySpriteLosingSanity;
            case "kySpritePanic":
                return kySpritePanic;
            case "kySpriteDefaultDetermined":
                return kySpriteDefaultDetermined;
            case "kySpriteDefaultDetermined2":
                return kySpriteDefaultDetermined2;
            case "kySpriteDefaultDetermined3":
                return kySpriteDefaultDetermined3;
            case "kySpriteDefaultDetermined4":
                return kySpriteDefaultDetermined4;
            case "kySpriteDefaultEmbarrassed":
                return kySpriteDefaultEmbarrased;
            case "kySpriteDefaultEmbarrassed2":
                return kySpriteDefaultEmbarrased2;
            case "kySpriteDefaultHappy":
                return kySpriteDefaultHappy;
            case "kySpriteDefaultHappy2":
                return kySpriteDefaultHappy2;
            case "kySpriteDefaultHappy3":
                return kySpriteDefaultHappy3;
            case "kySpriteDefaultLaughing":
                return kySpriteDefaultLaughing;
            case "kySpriteDefaultSheepish":
                return kySpriteDefaultSheepish;
            case "kySpriteDefaultSurprised":
                return kySpriteDefaultSurprised;
            case "kySpriteDefaultSurprised2":
                return kySpriteDefaultSurprised2;
            case "kySpriteDefaultWhiteLie":
                return kySpriteDefaultWhiteLie;
            case "kySpriteDefaultWorry":
                return kySpriteDefaultWorry;
            case "kySpriteDefault2Worry":
                return kySpriteDefault2Worry;
            case "kySpriteAnnoyed":
                return kySpriteAnnoyed;
            case "kySpriteAnxiousNoBag":
                return kySpriteAnxiousNoBag;
            case "kySpriteAnxious2NoBag":
                return kySpriteAnxious2NoBag;
            case "kySpriteAnxious3NoBag":
                return kySpriteAnxious3NoBag;
            case "kySpriteDeadpan":
                return kySpriteDeadpan;
            case "kySpriteExasperated":
                return kySpriteExasperated;
            case "kySpriteFear1NoBag":
                return kySpriteFear1NoBag;
            case "kySpriteFear3NoBag":
                return kySpriteFear3NoBag;
            case "kySpriteScared1NoBag":
                return kySpriteScared1NoBag;
            case "kySpriteScared2NoBag":
                return kySpriteScared2NoBag;
            case "kySpriteScared3NoBag":
                return kySpriteScared3NoBag;
            case "kySpriteWakeConfused":
                return kySpriteWakeConfused;
            case "kySpriteWakeHappy":
                return kySpriteWakeHappy;
            case "kySpriteWakeSheepish":
                return kySpriteWakeSheepish;
            case "kySpriteDefaultNoBag":
                return kySpriteDefaultNoBag;
            case "kySpriteDefaultDetermined4NoBag":
                return kySpriteDefaultDetermined4NoBag;
            case "kySpriteDefaultEmbarrassedNoBag":
                return kySpriteDefaultEmbarrasedNoBag;
            case "kySpriteDefaultEmbarrassed2NoBag":
                return kySpriteDefaultEmbarrased2NoBag;
            case "kySpriteDefaultHappyNoBag":
                return kySpriteDefaultHappyNoBag;
            case "kySpriteDefaultHappy3NoBag":
                return kySpriteDefaultHappy3NoBag;
            case "kySpriteDefaultLaughingNoBag":
                return kySpriteDefaultLaughingNoBag;
            case "kySpriteDefaultScaredAnxiousNoBag":
                return kySpriteDefaultScaredAnxiousNoBag;
            case "kySpriteDefaultScaredAnxious2NoBag":
                return kySpriteDefaultScaredAnxious2NoBag;
            case "kySpriteDefaultSheepishNoBag":
                return kySpriteDefaultSheepishNoBag;
            case "kySpriteDefaultSurprisedNoBag":
                return kySpriteDefaultSurprisedNoBag;
            case "kyNoSprite":
                return null;
            case "nikkoSpriteAnnoyed":
                return nikkoSpriteAnnoyed;
            case "nikkoSpriteAnnoyed2":
                return nikkoSpriteAnnoyed2;
            case "nikkoSpriteDefault":
                return nikkoSpriteDefault;
            case "nikkoSpriteDetermined":
                return nikkoSpriteDetermined;
            case "nikkoSpriteDetermined2":
                return nikkoSpriteDetermined2;
            case "nikkoSpriteEmbarrassed":
                return nikkoSpriteEmbarrassed;
            case "nikkoSpriteGrin":
                return nikkoSpriteGrin;
            case "nikkoSpriteHappy":
                return nikkoSpriteHappy;
            case "nikkoSpriteHappy2":
                return nikkoSpriteHappy2;
            case "nikkoSpritePanic":
                return nikkoSpritePanic;
            case "nikkoSpritePanic2":
                return nikkoSpritePanic2;
            case "nikkoSpriteRelieved":
                return nikkoSpriteRelieved;
            case "nikkoSpriteRelieved2":
                return nikkoSpriteRelieved2;
            case "nikkoSpriteSmirk1":
                return nikkoSpriteSmirk1;
            case "nikkoSpriteSmirk2":
                return nikkoSpriteSmirk2;
            case "nikkoSpriteSurprised":
                return nikkoSpriteSurprised;
            case "nikkoSpriteSurprised2":
                return nikkoSpriteSurprised2;
            case "nikkoSpriteWave":
                return nikkoSpriteWave;
            case "nikkoSpriteWorried1":
                return nikkoSpriteWorried1;
            case "nikkoSpriteWorried2":
                return nikkoSpriteWorried2;
            case "nikkoSpriteWorried3":
                return nikkoSpriteWorried3;
            case "nikkoSpriteWorried4":
                return nikkoSpriteWorried4;
            case "nikkoSpriteWorried5":
                return nikkoSpriteWorried5;
            case "nikkoSpriteWorried6":
                return nikkoSpriteWorried6;
            case "nikkoNoSprite":
                return null;
            case "simonSpriteComfort":
                return simonSpriteComfort;
            case "simonSpriteContent":
                return simonSpriteContent;
            case "simonSpriteDefault2":
                return simonSpriteDefault2;
            case "simonSpriteDefault":
                return simonSpriteDefault;
            case "simonSpriteEmbarrassed":
                return simonSpriteEmbarrassed;
            case "simonSpriteEmbarrassed2":
                return simonSpriteEmbarrassed2;
            case "simonSpriteExasperated":
                return simonSpriteExasperated;
            case "simonSpriteGuilt":
                return simonSpriteGuilt;
            case "simonSpriteHappy":
                return simonSpriteHappy;
            case "simonSpritePanic":
                return simonSpritePanic;
            case "simonSpritePanic2":
                return simonSpritePanic2;
            case "simonSpriteRelieved":
                return simonSpriteRelieved;
            case "simonSpriteSurprised":
                return simonSpriteSurprised;
            case "simonSpriteSurprised2":
                return simonSpriteSurprised2;
            case "simonSpriteWorried":
                return simonSpriteWorried;
            case "simonSpriteWorried2":
                return simonSpriteWorried2;
            case "simonSpriteWorried3":
                return simonSpriteWorried3;
            case "simonSpriteWorried4":
                return simonSpriteWorried4;
            case "simonSpriteWorried5":
                return simonSpriteWorried5;
            case "simonSpriteWorried6":
                return simonSpriteWorried6;
            case "simonSpriteWorried7":
                return simonSpriteWorried7;   
            case "simonNoSprite":
                return null;
            case "novaSpriteComfort":
                return novaSpriteComfort;
            case "novaSpriteDeadpan":
                return novaSpriteDeadpan;
            case "novaSpriteDetermined":
                return novaSpriteDetermined;
            case "novaSpriteFear":
                return novaSpriteFear;
            case "novaSpriteMad":
                return novaSpriteMad;
            case "novaSpriteNervous":
                return novaSpriteNervous;
            case "novaSpritePanic":
                return novaSpritePanic;
            case "novaSpriteSmile":
                return novaSpriteSmile;
            case "novaSpriteTaunt":
                return novaSpriteTaunt;
            case "novaSpriteWorried":
                return novaSpriteWorried;
            case "novaSpriteDefault":
                return novaSpriteDefault;
            case "novaNoSprite":
                return null;
            case "ayanaSpriteDefault":
                return ayanaSpriteDefault;
            case "ayanaSpriteHappy":
                return ayanaSpriteHappy;
            case "ayanaSpriteConfused":
                return ayanaSpriteConfused;
            case "ayanaSpriteShock":
                return ayanaSpriteShock;
            case "ayanaSpriteFear":
                return ayanaSpriteFear;
            case "ayanaSpriteAnxiousLookAway":
                return ayanaSpriteAnxiousLookAway;
            case "ayanaSpriteMad":
                return ayanaSpriteMad;
            case "ayanaSpriteDepressed":
                return ayanaSpriteDepressed;
            case "ayanaSpriteTearingUp":
                return ayanaSpriteTearingUp;
            case "ayanaSpriteCrying":
                return ayanaSpriteCrying;
            case "ayanaSpriteShockTearingUp":
                return ayanaSpriteShockTearingUp;
            case "ayanaSpriteTearySmile":
                return ayanaSpriteTearySmile;
            case "ayanaSpriteSheepish":
                return ayanaSpriteSheepish;
            case "ayanaSpriteWorriedLaugh":
                return ayanaSpriteWorriedLaugh;
            case "ayanaNoSprite":
                return null;
            case "ayanaSpriteMadTears":
                return ayanaSpriteMadTears;
        }
        return kySpriteDefault;
    }
}
