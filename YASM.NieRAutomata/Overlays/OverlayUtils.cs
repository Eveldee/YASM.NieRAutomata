using System.Numerics;
using ImGuiNET;
using YASM.NieRAutomata.Utils.Extensions;

namespace YASM.NieRAutomata.Overlays;

public partial class MainOverlay
{
    private static void HelpMarker(string description, bool sameLine = true)
    {
        if (sameLine)
        {
            ImGui.SameLine();
        }

        ImGui.TextDisabled("(?)");
        if (ImGui.IsItemHovered(ImGuiHoveredFlags.DelayShort) && ImGui.BeginTooltip())
        {
            ImGui.PushTextWrapPos(ImGui.GetFontSize() * 35.0f);
            ImGui.TextUnformatted(description);
            ImGui.PopTextWrapPos();
            ImGui.EndTooltip();
        }
    }

    private static bool ButtonColored(Vector4 color, string label, float gradientStep = 0.15f)
    {
        var gradient = color.IntensityGradient(gradientStep, 3);

        ImGui.PushStyleColor(ImGuiCol.Button, gradient[0]);
        ImGui.PushStyleColor(ImGuiCol.ButtonHovered, gradient[1]);
        ImGui.PushStyleColor(ImGuiCol.ButtonActive, gradient[2]);

        var result = ImGui.Button(label);

        ImGui.PopStyleColor(3);

        return result;
    }

    private static void TreeNodeEx(string label, Vector4 color, ImGuiTreeNodeFlags flags, Action content)
    {
        ImGui.PushStyleColor(ImGuiCol.Text, color);
        if (ImGui.TreeNodeEx(label, flags))
        {
            ImGui.PopStyleColor();

            content();

            ImGui.TreePop();
        }
        else
        {
            ImGui.PopStyleColor();
        }
    }
    private static void TreeNodeEx(string label, ImGuiTreeNodeFlags flags, Action content)
    {
        TreeNodeEx(label, InfoColor, flags, content);
    }

    private static void TreeNode(string label, Vector4 color, Action content)
    {
        ImGui.PushStyleColor(ImGuiCol.Text, color);
        if (ImGui.TreeNode(label))
        {
            ImGui.PopStyleColor();

            content();

            ImGui.TreePop();
        }
        else
        {
            ImGui.PopStyleColor();
        }
    }
    private static void TreeNode(string label, Action content)
    {
        TreeNode(label, InfoColor, content);
    }

    private static void Child(string label, Vector2 size, ImGuiChildFlags flags, Action content)
    {
        if (ImGui.BeginChild(label, size, flags))
        {
            content();
        }

        ImGui.EndChild();
    }

    private static void TabBar(string id, ImGuiTabBarFlags flags, Action content)
    {
        if (ImGui.BeginTabBar(id, flags))
        {
            content();

            ImGui.EndTabBar();
        }
    }

    private static void TabBar(string id, Action content)
    {
        TabBar(id, ImGuiTabBarFlags.None, content);
    }

    private static void TabItem(string label, Action content)
    {
        if (ImGui.BeginTabItem(label))
        {
            content();

            ImGui.EndTabItem();
        }
    }

    private static void TabItem(string label, ImGuiTabItemFlags flags, Action content)
    {
        bool open = true;
        if (ImGui.BeginTabItem(label, ref open, flags))
        {
            content();

            ImGui.EndTabItem();
        }
    }

    private static void WithDisabled(bool disabled, Action content)
    {
        if (disabled)
        {
            ImGui.BeginDisabled();
        }

        content();

        if (disabled)
        {
            ImGui.EndDisabled();
        }
    }
}