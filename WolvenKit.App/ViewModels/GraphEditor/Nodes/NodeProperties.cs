﻿using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using WolvenKit.App.Extensions;
using WolvenKit.RED4.Types;

namespace WolvenKit.App.ViewModels.GraphEditor.Nodes;

internal class NodeProperties
{
    public static Dictionary<string, string> GetPropertiesFor(questNodeDefinition? node)
    {
        Dictionary<string, string> details = new();

        details["Type"] = GetNameFromClass(node);

        if (node is questPauseConditionNodeDefinition pauseCondCasted)
        {
            details.AddRange(GetPropertiesForConditions(pauseCondCasted?.Condition?.Chunk));
        }
        else if (node is questConditionNodeDefinition condCasted)
        {
            details.AddRange(GetPropertiesForConditions(condCasted?.Condition?.Chunk));
        }
        else if (node is questFactsDBManagerNodeDefinition factDBManagerCasted)
        {
            if (factDBManagerCasted?.Type?.Chunk is questSetVar_NodeType setVarCasted)
            {
                details["Fact Name"] = setVarCasted?.FactName ?? "";
                details["Set Exact Value"] = setVarCasted?.SetExactValue == true ? "True" : "False";
                details["Value"] = setVarCasted?.Value.ToString() ?? "";
            }
        }
        else if (node is questJournalNodeDefinition journalNodeCasted)
        {
            if (journalNodeCasted?.Type?.Chunk is questJournalQuestEntry_NodeType journalQuestEntryCasted)
            {
                if (journalQuestEntryCasted?.Path?.Chunk is gameJournalPath gameJournalPath)
                {
                    details["Path Class Name"] = gameJournalPath?.ClassName.ToString()!;
                    details["Path File Entry Index"] = gameJournalPath?.FileEntryIndex.ToString()!;
                    details["Path Real Path"] = gameJournalPath?.RealPath.ToString()!;
                }

                details["Send Notification"] = journalQuestEntryCasted?.SendNotification == true ? "True" : "False";
                details["Track Quest"] = journalQuestEntryCasted?.TrackQuest == true ? "True" : "False";
                details["Version"] = journalQuestEntryCasted?.Version.ToEnumString()!;
            }
        }
        else if (node is questUseWorkspotNodeDefinition useWorkspotNodeCasted)
        {
            details["Entity Reference"] = ParseGameEntityReference(useWorkspotNodeCasted?.EntityReference);

            if (useWorkspotNodeCasted?.ParamsV1?.Chunk is scnUseSceneWorkspotParamsV1 useSceneWorkspotCasted)
            {
                details["Entry Id"] = useSceneWorkspotCasted?.EntryId?.Id.ToString()!;
                details["Exit Entry Id"] = useSceneWorkspotCasted?.ExitEntryId?.Id.ToString()!;
                details["Workspot Instance Id"] = useSceneWorkspotCasted?.WorkspotInstanceId?.Id.ToString()!;
            }
        }
        else if (node is questSceneManagerNodeDefinition sceneManagerNodeCasted)
        {
            if (sceneManagerNodeCasted?.Type?.Chunk is questSetTier_NodeType setTierCasted)
            {
                details["Force Empty Hands"] = setTierCasted?.ForceEmptyHands == true ? "True" : "False";
                details["Tier"] = setTierCasted?.Tier.ToEnumString()!;
            }
        }
        else if (node is questTimeManagerNodeDefinition timeManagerNodeCasted)
        {
            if (timeManagerNodeCasted?.Type?.Chunk is questPauseTime_NodeType pauseTimeCasted)
            {
                details["Pause"] = pauseTimeCasted?.Pause == true ? "True" : "False";
                details["Source"] = pauseTimeCasted?.Source.ToString()!;
            }
            if (timeManagerNodeCasted?.Type?.Chunk is questSetTime_NodeType setTimeCasted)
            {
                details["Hours"] = setTimeCasted?.Hours.ToString()!;
                details["Minutes"] = setTimeCasted?.Minutes.ToString()!;
                details["Seconds"] = setTimeCasted?.Seconds.ToString()!;
                details["Source"] = setTimeCasted?.Source.ToString()!;
            }
        }
        else if (node is questAudioNodeDefinition audioNodeCasted)
        {
            if (audioNodeCasted?.Type?.Chunk is questAudioMixNodeType audioMixCasted)
            {
                details["Mix Signpost"] = audioMixCasted?.MixSignpost.ToString()!;
            }
        }
        else if (node is questEventManagerNodeDefinition eventManagerNodeCasted)
        {
            details["Component Name"] = eventManagerNodeCasted?.ComponentName.ToString()!;
            details["Event"] = eventManagerNodeCasted?.Event?.Chunk?.GetType()?.Name!;
            details["Is Object Player"] = eventManagerNodeCasted?.IsObjectPlayer == true ? "True" : "False";
            details["Manager Name"] = eventManagerNodeCasted?.ManagerName.ToString()!;
            details["Object Ref"] = ParseGameEntityReference(eventManagerNodeCasted?.ObjectRef);
        }
        else if (node is questEnvironmentManagerNodeDefinition envManagerNodeCasted)
        {
            if (envManagerNodeCasted?.Type?.Chunk is questPlayEnv_SetWeather playEnvCasted)
            {
                details["Blend Time"] = playEnvCasted?.BlendTime.ToString()!;
                details["Priority"] = playEnvCasted?.Priority.ToString()!;
                details["Reset"] = playEnvCasted?.Reset == true ? "True" : "False";
                details["Source"] = playEnvCasted?.Source.ToString()!;
                details["Weather ID"] = playEnvCasted?.WeatherID.ResolvedText!;
            }
        }
        else if (node is questRenderFxManagerNodeDefinition renderFxManagerNodeCasted)
        {
            if (renderFxManagerNodeCasted?.Type?.Chunk is questSetFadeInOut_NodeType fadeInOutCasted)
            {
                details["Duration"] = fadeInOutCasted?.Duration.ToString()!;
                details["FadeColor"] = fadeInOutCasted?.FadeColor.ToString()!;
                details["Fade In"] = fadeInOutCasted?.FadeIn == true ? "True" : "False";
            }
        }
        else if (node is questUIManagerNodeDefinition uiManagerNodeCasted)
        {
            if (uiManagerNodeCasted?.Type?.Chunk is questSetHUDEntryForcedVisibility_NodeType hudVisibilityCasted)
            {
                if (hudVisibilityCasted?.HudEntryName != null)
                {
                    for (int i = 0; i < hudVisibilityCasted?.HudEntryName.Count; i++)
                    {
                        details["Hud Entry Name #" + i] = hudVisibilityCasted?.HudEntryName[i].ToString()!;
                    }
                }

                details["Hud Visibility Preset"] = hudVisibilityCasted?.HudVisibilityPreset.ResolvedText!;
                details["Skip Animation"] = hudVisibilityCasted?.SkipAnimation == true ? "True" : "False";
                details["Use Preset"] = hudVisibilityCasted?.UsePreset == true ? "True" : "False";
                details["Visibility"] = hudVisibilityCasted?.Visibility.ToEnumString()!;
            }
        }

        return details;
    }

    private static Dictionary<string, string> GetPropertiesForConditions(questIBaseCondition? node, string logicalCondIndex = "")
    {
        Dictionary<string, string> details = new();

        details[logicalCondIndex + "Condition type"] = GetNameFromClass(node);

        if (node is questTimeCondition condTimeCasted)
        {
            string ConditionTimeTypeName = logicalCondIndex + "Time condition";
            string ConditionTimeValTypeName = logicalCondIndex + "Time";

            if (condTimeCasted?.Type?.Chunk is questRealtimeDelay_ConditionType nodeRealtimeCondCasted)
            {
                details[ConditionTimeTypeName] = GetNameFromClass(nodeRealtimeCondCasted);
                details[ConditionTimeValTypeName] =
                    FormatNumber(nodeRealtimeCondCasted?.Hours) + ":" +
                    FormatNumber(nodeRealtimeCondCasted?.Minutes) + ":" +
                    FormatNumber(nodeRealtimeCondCasted?.Seconds) + "." +
                    FormatNumber(nodeRealtimeCondCasted?.Miliseconds, true);
            }

            if (condTimeCasted?.Type?.Chunk is questGameTimeDelay_ConditionType nodeGameTimeCondCasted)
            {
                details[ConditionTimeTypeName] = GetNameFromClass(nodeGameTimeCondCasted);
                details[ConditionTimeValTypeName] =
                    FormatNumber(nodeGameTimeCondCasted?.Days) + "d " +
                    FormatNumber(nodeGameTimeCondCasted?.Hours) + ":" +
                    FormatNumber(nodeGameTimeCondCasted?.Minutes) + ":" +
                    FormatNumber(nodeGameTimeCondCasted?.Seconds);
            }

            if (condTimeCasted?.Type?.Chunk is questTickDelay_ConditionType nodeTickCondCasted)
            {
                details[ConditionTimeTypeName] = GetNameFromClass(nodeTickCondCasted);
                details[ConditionTimeValTypeName] = FormatNumber(nodeTickCondCasted?.TickCount) + " ticks";
            }

            if (condTimeCasted?.Type?.Chunk is questTimePeriod_ConditionType nodeTimePeriodCondCasted)
            {
                details[ConditionTimeTypeName] = GetNameFromClass(nodeTimePeriodCondCasted);
                details[ConditionTimeValTypeName] =
                    "from " + FormatNumber(nodeTimePeriodCondCasted?.Begin?.Seconds) +
                    " to " + FormatNumber(nodeTimePeriodCondCasted?.End?.Seconds);
            }
        }
        else if (node is questFactsDBCondition condFactCasted)
        {
            if (condFactCasted?.Type?.Chunk is questVarComparison_ConditionType nodeVarCompCondCasted)
            {
                details[logicalCondIndex + "Fact Name"] = nodeVarCompCondCasted?.FactName!;
                details[logicalCondIndex + "Comparison Type"] = nodeVarCompCondCasted?.ComparisonType.ToEnumString()!;
                details[logicalCondIndex + "Value"] = nodeVarCompCondCasted?.Value.ToString()!;
            }

            if (condFactCasted?.Type?.Chunk is questVarVsVarComparison_ConditionType nodeVarVsVarCondCasted)
            {
                details[logicalCondIndex + "Comparison Type"] = nodeVarVsVarCondCasted?.ComparisonType.ToEnumString()!;
                details[logicalCondIndex + "Fact 1 Name"] = nodeVarVsVarCondCasted?.FactName1!;
                details[logicalCondIndex + "Fact 2 Name"] = nodeVarVsVarCondCasted?.FactName2!;
            }
        }
        else if (node is questLogicalCondition condLogicalCasted)
        {
            details[logicalCondIndex + "Operation"] = condLogicalCasted?.Operation.ToEnumString()!;
            if (condLogicalCasted?.Conditions != null)
            {
                for (int i = 0; i <  condLogicalCasted.Conditions.Count; i++)
                {
                    details.AddRange(GetPropertiesForConditions(condLogicalCasted.Conditions[i]?.Chunk, logicalCondIndex + "#" + i + " "));
                }
            }
        }
        else if (node is questCharacterCondition condCharacterCasted)
        {
            if (condCharacterCasted?.Type?.Chunk is questCharacterBodyType_CondtionType nodeCharBodyTypeCondCasted)
            {
                details[logicalCondIndex + "Gender"] = nodeCharBodyTypeCondCasted?.Gender.ToString()!;
                details[logicalCondIndex + "Object Ref"] = ParseGameEntityReference(nodeCharBodyTypeCondCasted?.ObjectRef);
                details[logicalCondIndex + "Is Player"] = nodeCharBodyTypeCondCasted?.IsPlayer == true ? "True" : "False";
            }
        }
        else if (node is questTriggerCondition condTriggerCasted)
        {
            details[logicalCondIndex + "Activator Ref"] = ParseGameEntityReference(condTriggerCasted?.ActivatorRef);
            details[logicalCondIndex + "Is Player Activator"] = condTriggerCasted?.IsPlayerActivator == true ? "True" : "False";
            details[logicalCondIndex + "Trigger Area Ref"] = condTriggerCasted?.TriggerAreaRef.GetResolvedText()!;
            details[logicalCondIndex + "Trigger Type"] = condTriggerCasted?.Type.ToEnumString()!;
        }
        else if (node is questSystemCondition condSystemCasted)
        {
            if (condSystemCasted?.Type?.Chunk is questCameraFocus_ConditionType nodeCameraFocusCondCasted)
            {
                details[logicalCondIndex + "Angle Tolerance"] = nodeCameraFocusCondCasted?.AngleTolerance.ToString()!;
                details[logicalCondIndex + "Inverted"] = nodeCameraFocusCondCasted?.Inverted == true ? "True" : "False";
                details[logicalCondIndex + "Object Ref"] = ParseGameEntityReference(nodeCameraFocusCondCasted?.ObjectRef);
                details[logicalCondIndex + "On Screen Test"] = nodeCameraFocusCondCasted?.OnScreenTest == true ? "True" : "False";
                details[logicalCondIndex + "Time Interval"] = nodeCameraFocusCondCasted?.TimeInterval.ToString()!;
                details[logicalCondIndex + "Use Frustrum Check"] = nodeCameraFocusCondCasted?.UseFrustrumCheck == true ? "True" : "False";
                details[logicalCondIndex + "Zoomed"] = nodeCameraFocusCondCasted?.Zoomed == true ? "True" : "False";
            }
        }

        return details;
    }

    private static string ParseGameEntityReference(gameEntityReference? entRef)
    {
        string str = "-";

        if (entRef != null)
        {
            if (entRef.DynamicEntityUniqueName != "None")
            {
                str = entRef.DynamicEntityUniqueName!;
            }
            if (entRef.Reference != 0)
            {
                str = entRef.Reference.GetResolvedText()!;
            }
        }

        return str;
    }

    private static string GetNameFromClass(RedBaseClass? node)
    {
        string name = "";

        if (node != null)
        {
            name = node.GetType().Name;

            name = name.Replace("quest", "");
            name = name.Replace("_ConditionType", "");
            name = name.Replace("NodeDefinition", "");

            //name = AddSpacesToSentence(name);
        }

        return name;
    }

    private static string FormatNumber(CUInt32? number, bool three = false)
    {
        if (number < 10 && !three)
        {
            return "0" + (number.ToString() ?? "0");
        }
        else if (number < 10 && three)
        {
            return "00" + (number.ToString() ?? "0");
        }
        else if (number < 100 && three)
        {
            return "0" + (number.ToString() ?? "0");
        }
        else
        {
            return (number.ToString() ?? (three ? "000" : "00"));
        }
    }

    private static string FormatNumber(CInt32? number, bool three = false)
    {
        if (number < 10 && !three)
        {
            return "0" + (number.ToString() ?? "0");
        }
        else if (number < 10 && three)
        {
            return "00" + (number.ToString() ?? "0");
        }
        else if (number < 100 && three)
        {
            return "0" + (number.ToString() ?? "0");
        }
        else
        {
            return (number.ToString() ?? (three ? "000" : "00"));
        }
    }
}
