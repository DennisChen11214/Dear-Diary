using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemInformation
{
    //What kind of item this is
    public enum ItemType {
        PuzzleKeyItem,
        PuzzleCreatePage,
        PuzzleDestroyPage,
        Diary,
        IDCard
    }

    //Information about the item
    public ItemType itemType;
    public string itemDescription;
    public bool keyItem;
    public bool equipItem;
    public int day;
    public int endDayPuzzle;
    public bool end;

    //Returns a sprite based on the type of the item
    public Sprite GetSprite(){
        switch(itemType){
            default:
            case ItemInformation.ItemType.PuzzleKeyItem : return ItemAssets.Instance.puzzleKeyItem;
            case ItemInformation.ItemType.PuzzleCreatePage: return ItemAssets.Instance.puzzleCreatePage;
            case ItemInformation.ItemType.PuzzleDestroyPage: return ItemAssets.Instance.puzzleDestroyPage;
            case ItemInformation.ItemType.Diary : return ItemAssets.Instance.diary;
            case ItemInformation.ItemType.IDCard: return ItemAssets.Instance.IDCard;
        }
    }

    //Returns a certain diary page sprite depending on the type of the item
    public Sprite GetDiaryPage(){
        switch(itemType){
            default:
            case ItemInformation.ItemType.PuzzleCreatePage : return GetCreatePage();
            case ItemInformation.ItemType.PuzzleDestroyPage : return GetDestroyPage();
            case ItemInformation.ItemType.Diary : return GetMainDiaryPage();
        }
    }

    //Returns a different creation diary page for each day 
    private Sprite GetCreatePage(){
        switch(day){
            case 1:
                return ItemAssets.Instance.puzzle1CreateDiaryPage;
            case 2:
                return ItemAssets.Instance.puzzle2CreateDiaryPage;
            case 3:
                return ItemAssets.Instance.puzzle3CreateDiaryPage;
            case 4:
                bool withSimon = DecisionManager.Instance.withSimonDay2;
                bool withNikko = DecisionManager.Instance.withNikkoDay3;
                //On the last day, return a different page depending if the player was with Nikko/Simon on previous days
                switch(endDayPuzzle){
                    case 1:
                        if(!withNikko && !withSimon){
                            return ItemAssets.Instance.puzzle4CreateDiaryPageAlone1;
                        }
                        else{
                            return ItemAssets.Instance.puzzle4CreateDiaryPage1;
                        }
                    case 2:
                        if(!withSimon){
                            return ItemAssets.Instance.puzzle4CreateDiaryPageAlone2;
                        }
                        else{
                            return ItemAssets.Instance.puzzle4CreateDiaryPage2;
                        }
                    case 3:
                        if(!withNikko){
                            return ItemAssets.Instance.puzzle4CreateDiaryPageAlone3;
                        }
                        else{
                            return ItemAssets.Instance.puzzle4CreateDiaryPage3;
                        }
                }
                return null;
        }
        return ItemAssets.Instance.puzzle1CreateDiaryPage;
    }

    //Returns a different destruction diary page for each day 
    private Sprite GetDestroyPage(){
        switch(day){
            case 1:
                return ItemAssets.Instance.puzzle1DestroyDiaryPage;
            case 2:
                return ItemAssets.Instance.puzzle2DestroyDiaryPage;
            case 3:
                return ItemAssets.Instance.puzzle3DestroyDiaryPage;
            case 4:
                //On the last day, return a different page depending if the player was with Nikko/Simon on previous days
                bool withSimon = DecisionManager.Instance.withSimonDay2;
                bool withNikko = DecisionManager.Instance.withNikkoDay3;
                switch(endDayPuzzle){
                    case 1:
                        return ItemAssets.Instance.puzzle4DestroyDiaryPage1;    
                    case 2:
                        return ItemAssets.Instance.puzzle4DestroyDiaryPage2;
                    case 3:
                        return ItemAssets.Instance.puzzle4DestroyDiaryPage3;
                }
                return null;
        }
        return ItemAssets.Instance.puzzle1DestroyDiaryPage;
    }

    //Returns the sprite of the diary page that the player gets at the end of each puzzle
    private Sprite GetMainDiaryPage(){
        switch(day){
            case 1:
                if(end)
                    return ItemAssets.Instance.day1DiaryPageEnd;
                return ItemAssets.Instance.day1DiaryPage;
            case 2:
                if(end)
                    return ItemAssets.Instance.day2DiaryPageEnd;
                return ItemAssets.Instance.day2DiaryPage;
            case 3:
                if(end)
                    return ItemAssets.Instance.day3DiaryPageEnd;
                return ItemAssets.Instance.day3DiaryPage;
            case 4:
                if(end)
                    return ItemAssets.Instance.day4DiaryPageEnd;
                return ItemAssets.Instance.day4DiaryPage;
        }
        return ItemAssets.Instance.day1DiaryPage;
    }

    //2 diary pages are equal if their sprites have the same name
    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }
        
        ItemInformation info = (ItemInformation) obj;
        if(itemType == ItemType.Diary){
            return GetDiaryPage().name == info.GetDiaryPage().name;
        }
        else
            return(info.itemType == itemType);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

}
