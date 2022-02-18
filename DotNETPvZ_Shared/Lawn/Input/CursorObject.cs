using System;
using Sexy;
using Sexy.TodLib;

namespace Lawn
{

    internal class CursorObject : GameObject
    {

        public CursorObject()
        {
            this.mType = SeedType.SEED_NONE;
            this.mImitaterType = SeedType.SEED_NONE;
            this.mSeedBankIndex = -1;
            this.mX = 0;
            this.mY = 0;
            this.mCursorType = CursorType.CURSOR_TYPE_NORMAL;
            this.mCoinID = null;
            this.mDuplicatorPlantID = null;
            this.mCobCannonPlantID = null;
            this.mGlovePlantID = null;
            this.mReanimCursorID = null;
            this.mPosScaled = false;
            if (this.mApp.IsWhackAZombieLevel())
            {
                ReanimatorXnaHelpers.ReanimatorEnsureDefinitionLoaded(ReanimationType.REANIM_HAMMER, true);
                Reanimation reanimation = this.mApp.AddReanimation(-25f, 16f, 0, ReanimationType.REANIM_HAMMER);
                reanimation.mIsAttachment = true;
                reanimation.PlayReanim(GlobalMembersReanimIds.ReanimTrackId_anim_whack_zombie, ReanimLoopType.REANIM_PLAY_ONCE_AND_HOLD, 0, 24f);
                reanimation.mAnimTime = 1f;
                this.mReanimCursorID = this.mApp.ReanimationGetID(reanimation);
            }
            this.mWidth = 80;
            this.mHeight = 80;
        }


        public void Update()
        {
            if (this.mApp.mGameScene != GameScenes.SCENE_PLAYING && !this.mBoard.mCutScene.IsInShovelTutorial())
            {
                this.mVisible = false;
                return;
            }
            if (!this.mApp.mWidgetManager.mMouseIn)
            {
                this.mVisible = false;
                return;
            }
            Reanimation reanimation = this.mApp.ReanimationTryToGet(this.mReanimCursorID);
            if (reanimation != null)
            {
                reanimation.Update();
            }
            this.mVisible = true;
            this.mX = this.mBoard.mLastToolX;
            this.mY = this.mBoard.mLastToolY;
        }


        public override bool LoadFromFile(Sexy.Buffer b)
        {
            base.LoadFromFile(b);
            this.mCursorType = (CursorType)b.ReadLong();
            this.mHammerDownCounter = b.ReadLong();
            this.mImitaterType = (SeedType)b.ReadLong();
            this.mSeedBankIndex = b.ReadLong();
            this.mType = (SeedType)b.ReadLong();
            return true;
        }


        public override bool SaveToFile(Sexy.Buffer b)
        {
            base.SaveToFile(b);
            b.WriteLong((int)this.mCursorType);
            b.WriteLong(this.mHammerDownCounter);
            b.WriteLong((int)this.mImitaterType);
            b.WriteLong(this.mSeedBankIndex);
            b.WriteLong((int)this.mType);
            return true;
        }


        public void DrawGroundLayer(Graphics g)
        {
            if (this.mCursorType != CursorType.CURSOR_TYPE_NORMAL)
            {
                int theX = (int)((float)this.mX * Constants.IS);
                int theY = (int)((float)this.mY * Constants.IS);
                int num;
                int num2;
                if (this.mCursorType == CursorType.CURSOR_TYPE_PLANT_FROM_BANK)
                {
                    if (this.mBoard.mIgnoreMouseUp)
                    {
                        return;
                    }
                    num = this.mBoard.PlantingPixelToGridX(theX, theY, this.mType);
                    num2 = this.mBoard.PlantingPixelToGridY(theX, theY, this.mType);
                    if (this.mBoard.CanPlantAt(num, num2, this.mType) != PlantingReason.PLANTING_OK)
                    {
                        return;
                    }
                }
                else
                {
                    num = this.mBoard.PixelToGridX(theX, theY);
                    num2 = this.mBoard.PixelToGridY(theX, theY);
                    if (this.mCursorType == CursorType.CURSOR_TYPE_SHOVEL && (this.mBoard.mIgnoreMouseUp || this.mBoard.ToolHitTest(this.mX, this.mY, false).mObjectType == GameObjectType.OBJECT_TYPE_NONE))
                    {
                        return;
                    }
                }
                if (num2 < 0 || num < 0 || this.mBoard.GridToPixelY(num, num2) < 0)
                {
                    return;
                }
                Graphics @new = Graphics.GetNew();
                @new.mTransX = Constants.Board_Offset_AspectRatio_Correction;
                for (int i = 0; i < Constants.GRIDSIZEX; i++)
                {
                    this.mBoard.DrawCelHighlight(@new, i, num2);
                }
                int num3 = this.mBoard.StageHas6Rows() ? 6 : 5;
                for (int j = 0; j < num3; j++)
                {
                    if (j != num2)
                    {
                        this.mBoard.DrawCelHighlight(@new, num, j);
                    }
                }
                @new.PrepareForReuse();
            }
        }

        public void DrawToolIconImage(Graphics g, Image image)
        {
            g.DrawImage(image, -image.mWidth / 2, -image.mHeight / 2, image.mWidth, image.mHeight);
        }

        public void DrawTopLayer(Graphics g)
        {
            
            switch (mCursorType)
            {
                case CursorType.CURSOR_TYPE_SHOVEL:
                    if (this.mBoard.mIgnoreMouseUp/*|| (float)this.mX * Constants.IS < (float)Constants.LAWN_XMIN || (float)this.mY * Constants.IS < (float)Constants.LAWN_YMIN*/)
                    {
                        return;
                    }
                    int mWidth = AtlasResources.IMAGE_SHOVEL_HI_RES.mWidth / 2;
                    int mHeight = AtlasResources.IMAGE_SHOVEL_HI_RES.mHeight / 2;
                    g.SetColor(SexyColor.White);
                    int num = (int)TodCommon.TodAnimateCurveFloat(0, 39, this.mBoard.mEffectCounter % 40, 0f, (float)(mWidth / 2), TodCurves.CURVE_BOUNCE_SLOW_MIDDLE);
                    g.DrawImage(AtlasResources.IMAGE_SHOVEL_HI_RES, num, -mHeight - num, mWidth, mHeight);
                    return;
                case CursorType.CURSOR_TYPE_HAMMER:
                    this.mApp.ReanimationGet(this.mReanimCursorID).Draw(g);
                    return;
                case CursorType.CURSOR_TYPE_COBCANNON_TARGET:
                    if ((float)this.mX * Constants.IS < (float)Constants.LAWN_XMIN || (float)this.mY * Constants.IS < (float)Constants.LAWN_YMIN)
                    {
                        return;
                    }
                    int mWidth3 = AtlasResources.IMAGE_COBCANNON_TARGET.mWidth / 2;
                    int mHeight3 = AtlasResources.IMAGE_COBCANNON_TARGET.mHeight / 2;
                    g.SetColorizeImages(true);
                    g.SetColor(new SexyColor(255, 255, 255, 127));
                    g.DrawImage(AtlasResources.IMAGE_COBCANNON_TARGET, -mWidth3 / 2, -mHeight3 / 2, mWidth3, mHeight3);
                    g.SetColorizeImages(false);
                    return;
                case CursorType.CURSOR_TYPE_WATERING_CAN:
                    if (this.mApp.mPlayerInfo.mPurchases[13] > 0)
                    {
                        TRect trect = new TRect(Constants.ZEN_XMIN, Constants.ZEN_YMIN, Constants.ZEN_XMAX - Constants.ZEN_XMIN, Constants.ZEN_YMAX - Constants.ZEN_YMIN);
                        if (trect.Contains(this.mApp.mBoard.mLastToolX, this.mApp.mBoard.mLastToolY))
                        {
                            int mWidth2 = AtlasResources.IMAGE_ZEN_GOLDTOOLRETICLE.mWidth;
                            int mHeight2 = AtlasResources.IMAGE_ZEN_GOLDTOOLRETICLE.mHeight;
                            g.DrawImage(AtlasResources.IMAGE_ZEN_GOLDTOOLRETICLE, -mWidth2 / 2, -mHeight2 / 2, mWidth2, mHeight2);
                        }
                        DrawToolIconImage(g, AtlasResources.IMAGE_REANIM_ZENGARDEN_WATERINGCAN1_GOLD);
                    }
                    else 
                    {
                        DrawToolIconImage(g, AtlasResources.IMAGE_REANIM_ZENGARDEN_WATERINGCAN1);
                    }
                    break;
                case CursorType.CURSOR_TYPE_BUG_SPRAY:
                    DrawToolIconImage(g, AtlasResources.IMAGE_REANIM_ZENGARDEN_BUGSPRAY_BOTTLE);
                    return;
                case CursorType.CURSOR_TYPE_PHONOGRAPH:
                    DrawToolIconImage(g, AtlasResources.IMAGE_PHONOGRAPH);
                    return;
                case CursorType.CURSOR_TYPE_FERTILIZER:
                    DrawToolIconImage(g, AtlasResources.IMAGE_REANIM_ZENGARDEN_FERTILIZER_BAG1);
                    return;
                case CursorType.CURSOR_TYPE_GLOVE:
                    DrawToolIconImage(g, AtlasResources.IMAGE_ZEN_GARDENGLOVE);
                    return;
                case CursorType.CURSOR_TYPE_CHOCOLATE:
                    DrawToolIconImage(g, AtlasResources.IMAGE_CHOCOLATE);
                    return;
                case CursorType.CURSOR_TYPE_MONEY_SIGN:
                    DrawToolIconImage(g, AtlasResources.IMAGE_ZEN_MONEYSIGN);
                    return;
                case CursorType.CURSOR_TYPE_WHEEELBARROW:
                    Image wbimage = AtlasResources.IMAGE_ZEN_WHEELBARROW;
                    DrawToolIconImage(g, wbimage);
                    PottedPlant wbplant = this.mApp.mZenGarden.GetPottedPlantInWheelbarrow();
                    if (wbplant != null)
                    {
                        this.mApp.mZenGarden.DrawPottedPlant(g, -wbimage.mWidth/2 + (float)(Constants.ZenGardenButton_WheelbarrowPlant_Offset.X), -wbimage.mHeight / 2 + (float)(Constants.ZenGardenButton_WheelbarrowPlant_Offset.Y), wbplant, 0.6f, true);
                    }
                    return;
                case CursorType.CURSOR_TYPE_PLANT_FROM_BANK:
                case CursorType.CURSOR_TYPE_PLANT_FROM_USABLE_COIN:
                    if (this.mBoard.mIgnoreMouseUp || (float)this.mX * Constants.IS < (float)Constants.LAWN_XMIN || (float)this.mY * Constants.IS < (float)Constants.LAWN_YMIN)
                    {
                        return;
                    }
                    int xPos = -40;
                    int yPos = -40;
                    if (Challenge.IsZombieSeedType(mType))
                    {
                        ZombieType theZombieType = Challenge.IZombieSeedTypeToZombieType(mType);
                        GlobalStaticVars.gLawnApp.mReanimatorCache.DrawCachedZombie(g, xPos, yPos, theZombieType);
                        return;
                    }
                    PlantDefinition plantDef = Plant.GetPlantDefinition(mType);
                    Reanimation plantReanim = this.mApp.AddReanimation(xPos, yPos, 0, plantDef.mReanimationType);
                    plantReanim.mIsAttachment = true;
                    plantReanim.SetFramesForLayer(GlobalMembersReanimIds.ReanimTrackId_anim_idle);
                    plantReanim.mAnimTime = 1f;
                    //this.mReanimCursorID = this.mApp.ReanimationGetID(plantReanim);
                    SeedType theSeedType = mType;
                    if (theSeedType == SeedType.SEED_PEASHOOTER || theSeedType == SeedType.SEED_SNOWPEA || theSeedType == SeedType.SEED_REPEATER || theSeedType == SeedType.SEED_LEFTPEATER || theSeedType == SeedType.SEED_GATLINGPEA)
                    {
                        plantReanim.mAnimRate = TodCommon.RandRangeFloat(15f, 20f);
                        Reanimation reanimation2 = this.mApp.AddReanimation(xPos, yPos, this.mRenderOrder + 2, Plant.GetPlantDefinition(mType).mReanimationType);
                        reanimation2.mLoopType = ReanimLoopType.REANIM_LOOP;
                        reanimation2.mAnimRate = plantReanim.mAnimRate;
                        reanimation2.SetFramesForLayer(GlobalMembersReanimIds.ReanimTrackId_anim_head_idle);
                        reanimation2.AttachToAnotherReanimation(ref plantReanim, GlobalMembersReanimIds.ReanimTrackId_anim_idle);
                    }
                    else if (theSeedType == SeedType.SEED_SPLITPEA)
                    {
                        Debug.ASSERT(plantReanim != null);
                        plantReanim.mAnimRate = TodCommon.RandRangeFloat(15f, 20f);
                        Reanimation reanimation3 = this.mApp.AddReanimation(xPos, yPos, this.mRenderOrder + 2, plantDef.mReanimationType);
                        reanimation3.mLoopType = ReanimLoopType.REANIM_LOOP;
                        reanimation3.mAnimRate = plantReanim.mAnimRate;
                        reanimation3.SetFramesForLayer(GlobalMembersReanimIds.ReanimTrackId_anim_head_idle);
                        reanimation3.AttachToAnotherReanimation(ref plantReanim, GlobalMembersReanimIds.ReanimTrackId_anim_idle);
                        Reanimation reanimation4 = this.mApp.AddReanimation(xPos, yPos, this.mRenderOrder + 2, plantDef.mReanimationType);
                        reanimation4.mLoopType = ReanimLoopType.REANIM_LOOP;
                        reanimation4.mAnimRate = plantReanim.mAnimRate;
                        reanimation4.SetFramesForLayer(GlobalMembersReanimIds.ReanimTrackId_anim_splitpea_idle);
                        reanimation4.AttachToAnotherReanimation(ref plantReanim, GlobalMembersReanimIds.ReanimTrackId_anim_idle);
                    }
                    else if (theSeedType == SeedType.SEED_THREEPEATER)
                    {
                        Debug.ASSERT(plantReanim != null);
                        plantReanim.mAnimRate = TodCommon.RandRangeFloat(15f, 20f);
                        Reanimation reanimation5 = this.mApp.AddReanimation(xPos, yPos, this.mRenderOrder + 2, plantDef.mReanimationType);
                        reanimation5.mLoopType = ReanimLoopType.REANIM_LOOP;
                        reanimation5.mAnimRate = plantReanim.mAnimRate;
                        reanimation5.SetFramesForLayer(GlobalMembersReanimIds.ReanimTrackId_anim_head_idle1);
                        reanimation5.AttachToAnotherReanimation(ref plantReanim, GlobalMembersReanimIds.ReanimTrackId_anim_head1);
                        Reanimation reanimation6 = this.mApp.AddReanimation(xPos, yPos, this.mRenderOrder + 2, plantDef.mReanimationType);
                        reanimation6.mLoopType = ReanimLoopType.REANIM_LOOP;
                        reanimation6.mAnimRate = plantReanim.mAnimRate;
                        reanimation6.SetFramesForLayer(GlobalMembersReanimIds.ReanimTrackId_anim_head_idle2);
                        reanimation6.AttachToAnotherReanimation(ref plantReanim, GlobalMembersReanimIds.ReanimTrackId_anim_head2);
                        Reanimation reanimation7 = this.mApp.AddReanimation(xPos, yPos, this.mRenderOrder + 2, plantDef.mReanimationType);
                        reanimation7.mLoopType = ReanimLoopType.REANIM_LOOP;
                        reanimation7.mAnimRate = plantReanim.mAnimRate;
                        reanimation7.SetFramesForLayer(GlobalMembersReanimIds.ReanimTrackId_anim_head_idle3);
                        reanimation7.AttachToAnotherReanimation(ref plantReanim, GlobalMembersReanimIds.ReanimTrackId_anim_head3);
                    }
                    plantReanim.Draw(g);
                    return;
                default:
                    break;
            }
            return;
        }


        public void Die()
        {
            this.mApp.RemoveReanimation(ref this.mReanimCursorID);
        }


        public int mSeedBankIndex;


        public SeedType mType;


        public SeedType mImitaterType;


        public CursorType mCursorType;


        public Coin mCoinID;


        public Plant mGlovePlantID;


        public Plant mDuplicatorPlantID;


        public Plant mCobCannonPlantID;


        public int mHammerDownCounter;


        public Reanimation mReanimCursorID;
    }
}
