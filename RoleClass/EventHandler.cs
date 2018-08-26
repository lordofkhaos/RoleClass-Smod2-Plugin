﻿using Smod2;
using Smod2.API;
using Smod2.EventHandlers;
using Smod2.Events;
using Smod2.EventSystem.Events;
using System;
using System.IO;
using System.Collections.Generic;
using System.Collections;
using System.Collections.Specialized;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
//using System.Xml;
//using System.Xml.Serialization;

namespace RoleClass
{
    class EventHandler : IEventHandlerPlayerJoin, IEventHandlerRoundStart, IEventHandlerSetRole
    {
        readonly Plugin plugin;
        //private Player player;

        public EventHandler(Plugin plugin)
        {
            this.plugin = plugin;
        }

        public class Info
        {
            public List<Dictionary<string, Role>> SCPs { get; set; }
            public List<Dictionary<string, Role>> Humans { get; set; }
            public List<Dictionary<string, Role>> Other { get; set; }
            public List<List<Dictionary<string, Role>>> Cls { get; set; }
            public List<Dictionary<string, ItemType>> Keycards { get; set; }
            public List<Dictionary<string, ItemType>> Weapons { get; set; }
            public List<Dictionary<string, ItemType>> Ammo { get; set; }
            public List<Dictionary<string, ItemType>> Accessories { get; set; }
            public List<List<Dictionary<string, ItemType>>> Masteritems { get; set; }
        }

        public void OnRoundStart(RoundStartEvent ev)
        {
            Info info = new Info();
            string[] players = new string[ev.Server.GetPlayers().Count];
            #region dare ordini agnomes
            // prae perficio
            info.SCPs = new List<Dictionary<string, Role>>();
            info.Humans = new List<Dictionary<string, Role>>();
            info.Other = new List<Dictionary<string, Role>>();
            info.Cls = new List<List<Dictionary<string, Role>>>();
            // perficio
            var ntfc = Role.NTF_COMMANDER.ToString().ToLower();
            Dictionary<string, Role> mtfc = new Dictionary<string, Role>()
            {
                ["mtfc"] = Role.NTF_COMMANDER,
                ["ntfc"] = Role.NTF_COMMANDER,
                ["commander"] = Role.NTF_COMMANDER,
                ["12"] = Role.NTF_COMMANDER,
                [ntfc] = Role.NTF_COMMANDER
            };
            info.Humans.Add(mtfc);

            var ntfl = Role.NTF_LIEUTENANT.ToString().ToLower();
            Dictionary<string, Role> mtfl = new Dictionary<string, Role>()
            {
                ["mtfl"] = Role.NTF_LIEUTENANT,
                ["ntfl"] = Role.NTF_LIEUTENANT,
                ["lieutenant"] = Role.NTF_LIEUTENANT,
                ["11"] = Role.NTF_LIEUTENANT,
                [ntfl] = Role.NTF_LIEUTENANT
            };
            info.Humans.Add(mtfl);

            var ntf = Role.NTF_CADET.ToString().ToLower();
            Dictionary<string, Role> mtfcad = new Dictionary<string, Role>()
            {
                ["cadet"] = Role.NTF_CADET,
                ["mtf"] = Role.NTF_CADET,
                ["ntf"] = Role.NTF_CADET,
                ["13"] = Role.NTF_CADET,
                [ntf] = Role.NTF_CADET
            };
            info.Humans.Add(mtfcad);

            var ntfs = Role.NTF_SCIENTIST.ToString().ToLower();
            Dictionary<string, Role> mtfs = new Dictionary<string, Role>()
            {
                ["mtfs"] = Role.NTF_SCIENTIST,
                ["ntfs"] = Role.NTF_SCIENTIST,
                ["mtfsci"] = Role.NTF_SCIENTIST,
                ["ntfsci"] = Role.NTF_SCIENTIST,
                ["4"] = Role.NTF_SCIENTIST,
                [ntfs] = Role.NTF_SCIENTIST
            };
            info.Humans.Add(mtfs);

            var ci = Role.CHAOS_INSUGENCY.ToString().ToLower();
            Dictionary<string, Role> ceeeye = new Dictionary<string, Role>()
            {
                ["ci"] = Role.CHAOS_INSUGENCY,
                ["chaos"] = Role.CHAOS_INSUGENCY,
                ["insurgency"] = Role.CHAOS_INSUGENCY,
                ["insurgent"] = Role.CHAOS_INSUGENCY,
                ["8"] = Role.CHAOS_INSUGENCY,
                [ci] = Role.CHAOS_INSUGENCY
            };
            info.Humans.Add(ceeeye);

            var cd = Role.CLASSD.ToString().ToLower();
            Dictionary<string, Role> dclass = new Dictionary<string, Role>()
            {
                ["classd"] = Role.CLASSD,
                ["class-d"] = Role.CLASSD,
                ["dclass"] = Role.CLASSD,
                ["d-class"] = Role.CLASSD,
                ["cd"] = Role.CLASSD,
                ["dc"] = Role.CLASSD,
                ["personnel"] = Role.CLASSD,
                ["1"] = Role.CLASSD,
                [cd] = Role.CLASSD
            };
            info.Humans.Add(dclass);

            var fg = Role.FACILITY_GUARD.ToString().ToLower();
            Dictionary<string, Role> guard = new Dictionary<string, Role>()
            {
                ["guard"] = Role.FACILITY_GUARD,
                ["fg"] = Role.FACILITY_GUARD,
                ["15"] = Role.FACILITY_GUARD,
                [fg] = Role.FACILITY_GUARD
            };
            info.Humans.Add(guard);

            var sci = Role.SCIENTIST.ToString().ToLower();
            Dictionary<string, Role> nerd = new Dictionary<string, Role>()
            {
                ["sci"] = Role.SCIENTIST,
                ["nerd"] = Role.SCIENTIST,
                ["scientist"] = Role.SCIENTIST,
                ["science"] = Role.SCIENTIST,
                ["science"] = Role.SCIENTIST,
                ["6"] = Role.SCIENTIST,
                [sci] = Role.SCIENTIST
            };
            info.Humans.Add(nerd);

            var plaguedaddy = Role.SCP_049.ToString().ToLower();
            Dictionary<string, Role> s049 = new Dictionary<string, Role>()
            {
                ["plaguedaddy"] = Role.SCP_049,
                ["doctor"] = Role.SCP_049,
                ["scp049"] = Role.SCP_049,
                ["scp-049"] = Role.SCP_049,
                ["scp_049"] = Role.SCP_049,
                ["049"] = Role.SCP_049,
                ["5"] = Role.SCP_049,
                [plaguedaddy] = Role.SCP_049
            };
            info.SCPs.Add(s049);

            var zombie = Role.SCP_049_2.ToString().ToLower();
            Dictionary<string, Role> s049_2 = new Dictionary<string, Role>()
            {
                ["zombie"] = Role.SCP_049_2,
                ["scp0492"] = Role.SCP_049_2,
                ["scp-049-2"] = Role.SCP_049_2,
                ["scp-049_2"] = Role.SCP_049_2,
                ["scp_049-2"] = Role.SCP_049_2,
                ["scp_049_2"] = Role.SCP_049_2,
                ["scp049-2"] = Role.SCP_049_2,
                ["scp049_2"] = Role.SCP_049_2,
                ["helicopter"] = Role.SCP_049_2,
                ["10"] = Role.SCP_049_2,
                [zombie] = Role.SCP_049_2
            };
            info.SCPs.Add(s049_2);

            var larry = Role.SCP_106.ToString().ToLower();
            Dictionary<string, Role> s106 = new Dictionary<string, Role>()
            {
                ["larry"] = Role.SCP_106,
                ["scp-106"] = Role.SCP_106,
                ["scp_106"] = Role.SCP_106,
                ["scp106"] = Role.SCP_106,
                ["106"] = Role.SCP_106,
                ["3"] = Role.SCP_106,
                [larry] = Role.SCP_106
            };
            info.SCPs.Add(s106);

            var shyguy = Role.SCP_096.ToString().ToLower();
            Dictionary<string, Role> s096 = new Dictionary<string, Role>()
            {
                ["shyguy"] = Role.SCP_096,
                ["096"] = Role.SCP_096,
                ["scp-096"] = Role.SCP_096,
                ["scp_096"] = Role.SCP_096,
                ["scp096"] = Role.SCP_096,
                ["9"] = Role.SCP_096,
                [shyguy] = Role.SCP_096
            };
            info.SCPs.Add(s096);

            var comp = Role.SCP_079.ToString().ToLower();
            Dictionary<string, Role> s079 = new Dictionary<string, Role>()
            {
                ["scp079"] = Role.SCP_079,
                ["scp-079"] = Role.SCP_079,
                ["scp_079"] = Role.SCP_079,
                ["comp"] = Role.SCP_079,
                ["computer"] = Role.SCP_079,
                ["7"] = Role.SCP_079,
                [comp] = Role.SCP_079
            };
            info.SCPs.Add(s079);

            var peanut = Role.SCP_173.ToString().ToLower();
            Dictionary<string, Role> s173 = new Dictionary<string, Role>()
            {
                ["peanut"] = Role.SCP_173,
                ["scp173"] = Role.SCP_173,
                ["scp-173"] = Role.SCP_173,
                ["scp_173"] = Role.SCP_173,
                ["0"] = Role.SCP_173,
                [peanut] = Role.SCP_173
            };
            info.SCPs.Add(s173);

            var doggo1 = Role.SCP_939_53.ToString().ToLower();
            Dictionary<string, Role> s93953 = new Dictionary<string, Role>()
            {
                ["939-53"] = Role.SCP_939_53,
                ["939_53"] = Role.SCP_939_53,
                ["scp93953"] = Role.SCP_939_53,
                ["scp-939-53"] = Role.SCP_939_53,
                ["scp_939_53"] = Role.SCP_939_53,
                ["16"] = Role.SCP_939_53,
                [doggo1] = Role.SCP_939_53
            };
            info.SCPs.Add(s93953);

            var doggo2 = Role.SCP_939_89.ToString().ToLower();
            Dictionary<string, Role> s93989 = new Dictionary<string, Role>()
            {
                ["939-89"] = Role.SCP_939_89,
                ["939_89"] = Role.SCP_939_89,
                ["scp93989"] = Role.SCP_939_89,
                ["scp-939-89"] = Role.SCP_939_89,
                ["scp_939_89"] = Role.SCP_939_89,
                ["17"] = Role.SCP_939_89,
                [doggo2] = Role.SCP_939_89
            };
            info.SCPs.Add(s93989);

            var tut = Role.TUTORIAL.ToString().ToLower();
            Dictionary<string, Role> tutor = new Dictionary<string, Role>()
            {
                ["tut"] = Role.TUTORIAL,
                ["tutor"] = Role.TUTORIAL,
                ["tutorial"] = Role.TUTORIAL,
                ["14"] = Role.TUTORIAL,
                [tut] = Role.TUTORIAL
            };
            info.Humans.Add(tutor);

            var spec = Role.SPECTATOR.ToString().ToLower();
            Dictionary<string, Role> ghost = new Dictionary<string, Role>()
            {
                ["spec"] = Role.SPECTATOR,
                ["specboi"] = Role.SPECTATOR,
                ["ghost"] = Role.SPECTATOR,
                ["2"] = Role.SPECTATOR,
                [spec] = Role.SPECTATOR
            };
            info.Other.Add(ghost);

            var un = Role.UNASSIGNED.ToString().ToLower();
            Dictionary<string, Role> unass = new Dictionary<string, Role>()
            {
                ["un"] = Role.UNASSIGNED,
                ["none"] = Role.UNASSIGNED,
                [un] = Role.UNASSIGNED
            };
            info.Other.Add(unass);

            info.Cls.Add(info.Humans);
            info.Cls.Add(info.SCPs);
            info.Cls.Add(info.Other);

            foreach (List<Dictionary<string, Role>> x in info.Cls)
            {
                foreach (Dictionary<string, Role> v in x)
                {
                    string[] m = v.Keys.ToArray();
                    foreach (string s in m)
                        plugin.Debug(s);
                    string n = v.Values.ToString();
                    plugin.Debug(n);
                }

            }
            #endregion
            #region dare rei agnomes
            // prae perficio
            info.Keycards = new List<Dictionary<string, ItemType>>();
            info.Weapons = new List<Dictionary<string, ItemType>>();
            info.Ammo = new List<Dictionary<string, ItemType>>();
            info.Accessories = new List<Dictionary<string, ItemType>>();
            info.Masteritems = new List<List<Dictionary<string, ItemType>>>();
            // perficio
            var kc_ci = ItemType.CHAOS_INSURGENCY_DEVICE.ToString().ToLower();
            Dictionary<string, ItemType> cidev = new Dictionary<string, ItemType>()
            {
                ["ci_device"] = ItemType.CHAOS_INSURGENCY_DEVICE,
                ["ci-device"] = ItemType.CHAOS_INSURGENCY_DEVICE,
                ["cidevice"] = ItemType.CHAOS_INSURGENCY_DEVICE,
                ["kc-ci"] = ItemType.CHAOS_INSURGENCY_DEVICE,
                ["ci"] = ItemType.CHAOS_INSURGENCY_DEVICE,
                [kc_ci] = ItemType.CHAOS_INSURGENCY_DEVICE
            };
            info.Keycards.Add(cidev);

            var coin = ItemType.COIN.ToString().ToLower();
            Dictionary<string, ItemType> money = new Dictionary<string, ItemType>()
            {
                ["coin"] = ItemType.COIN,
                ["quarter"] = ItemType.COIN,
                ["25c"] = ItemType.COIN,
                ["50c"] = ItemType.COIN,
                ["17"] = ItemType.COIN,
                [coin] = ItemType.COIN
            };
            info.Accessories.Add(money);

            var pew = ItemType.COM15.ToString().ToLower();
            Dictionary<string, ItemType> com15 = new Dictionary<string, ItemType>()
            {
                ["pistol"] = ItemType.COM15,
                ["com15"] = ItemType.COM15,
                ["handgun"] = ItemType.COM15,
                ["13"] = ItemType.COM15,
                [pew] = ItemType.COM15
            };
            info.Weapons.Add(com15);

            var kc_ce = ItemType.CONTAINMENT_ENGINEER_KEYCARD.ToString().ToLower();
            Dictionary<string, ItemType> conteng = new Dictionary<string, ItemType>()
            {
                ["ce"] = ItemType.CONTAINMENT_ENGINEER_KEYCARD,
                ["containment_engineer"] = ItemType.CONTAINMENT_ENGINEER_KEYCARD,
                ["containment-engineer"] = ItemType.CONTAINMENT_ENGINEER_KEYCARD,
                ["pinkcard"] = ItemType.CONTAINMENT_ENGINEER_KEYCARD,
                ["pink"] = ItemType.CONTAINMENT_ENGINEER_KEYCARD,
                ["kc-ce"] = ItemType.CONTAINMENT_ENGINEER_KEYCARD,
                ["6"] = ItemType.CONTAINMENT_ENGINEER_KEYCARD,
                [kc_ce] = ItemType.CONTAINMENT_ENGINEER_KEYCARD
            };
            info.Keycards.Add(conteng);

            var cup = ItemType.CUP.ToString().ToLower();
            Dictionary<string, ItemType> dictcup = new Dictionary<string, ItemType>()
            {
                ["cup"] = ItemType.CUP,
                ["18"] = ItemType.CUP,
                [cup] = ItemType.CUP
            };
            info.Accessories.Add(dictcup);

            var disarm = ItemType.DISARMER.ToString().ToLower();
            Dictionary<string, ItemType> disarmer = new Dictionary<string, ItemType>()
            {
                ["disarm"] = ItemType.DISARMER,
                ["disarmer"] = ItemType.DISARMER,
                ["detainer"] = ItemType.DISARMER,
                ["detain"] = ItemType.DISARMER,
                ["handcuffs"] = ItemType.DISARMER,
                ["27"] = ItemType.DISARMER,
                [disarm] = ItemType.DISARMER
            };
            info.Accessories.Add(disarmer);

            var fusion = ItemType.DROPPED_5.ToString().ToLower();
            Dictionary<string, ItemType> e11ammo = new Dictionary<string, ItemType>()
            {
                ["fusion"] = ItemType.DROPPED_5,
                ["mtfammo"] = ItemType.DROPPED_5,
                ["ntfammo"] = ItemType.DROPPED_5,
                ["e11ammo"] = ItemType.DROPPED_5,
                ["ammo5"] = ItemType.DROPPED_5,
                ["556mm"] = ItemType.DROPPED_5,
                ["22"] = ItemType.DROPPED_5,
                [fusion] = ItemType.DROPPED_5
            };
            info.Ammo.Add(e11ammo);

            var pat = ItemType.DROPPED_7.ToString().ToLower();
            Dictionary<string, ItemType> mm9 = new Dictionary<string, ItemType>()
            {
                ["9mm"] = ItemType.DROPPED_7,
                ["pat"] = ItemType.DROPPED_7,
                ["ammo7"] = ItemType.DROPPED_7,
                ["29"] = ItemType.DROPPED_7,
                [pat] = ItemType.DROPPED_7
            };
            info.Ammo.Add(mm9);

            var rat = ItemType.DROPPED_9.ToString().ToLower();
            Dictionary<string, ItemType> mm762 = new Dictionary<string, ItemType>()
            {
                ["762mm"] = ItemType.DROPPED_9,
                ["rat"] = ItemType.DROPPED_9,
                ["ammo9"] = ItemType.DROPPED_9,
                ["28"] = ItemType.DROPPED_9,
                [rat] = ItemType.DROPPED_9
            };
            info.Ammo.Add(mm762);

            var e11 = ItemType.E11_STANDARD_RIFLE.ToString().ToLower();
            Dictionary<string, ItemType> esr = new Dictionary<string, ItemType>()
            {
                ["mtfgun"] = ItemType.E11_STANDARD_RIFLE,
                ["ntfgun"] = ItemType.E11_STANDARD_RIFLE,
                ["epsilon"] = ItemType.E11_STANDARD_RIFLE,
                ["epsilon11"] = ItemType.E11_STANDARD_RIFLE,
                ["epsilonstandard"] = ItemType.E11_STANDARD_RIFLE,
                ["epsilon11standard"] = ItemType.E11_STANDARD_RIFLE,
                ["epsilonstandardrifle"] = ItemType.E11_STANDARD_RIFLE,
                ["epsilon11standardrifle"] = ItemType.E11_STANDARD_RIFLE,
                ["epsilonrifle"] = ItemType.E11_STANDARD_RIFLE,
                ["epsilon11rifle"] = ItemType.E11_STANDARD_RIFLE,
                ["esr"] = ItemType.E11_STANDARD_RIFLE,
                ["20"] = ItemType.E11_STANDARD_RIFLE,
                [e11] = ItemType.E11_STANDARD_RIFLE
            };
            info.Weapons.Add(esr);

            var kc_fm = ItemType.FACILITY_MANAGER_KEYCARD.ToString().ToLower();
            Dictionary<string, ItemType> red = new Dictionary<string, ItemType>()
            {
                ["9"] = ItemType.FACILITY_MANAGER_KEYCARD,
                ["red"] = ItemType.FACILITY_MANAGER_KEYCARD,
                ["kc=fm"] = ItemType.FACILITY_MANAGER_KEYCARD,
                ["redcard"] = ItemType.FACILITY_MANAGER_KEYCARD,
                [kc_fm] = ItemType.FACILITY_MANAGER_KEYCARD
            };
            info.Keycards.Add(red);

            var fb = ItemType.FLASHBANG.ToString().ToLower();
            Dictionary<string, ItemType> sg = new Dictionary<string, ItemType>()
            {
                ["26"] = ItemType.FLASHBANG,
                ["fb"] = ItemType.FLASHBANG,
                ["stun"] = ItemType.FLASHBANG,
                ["flashbang"] = ItemType.FLASHBANG,
                ["stungrenade"] = ItemType.FLASHBANG,
                ["flashgrenade"] = ItemType.FLASHBANG,
                [fb] = ItemType.FLASHBANG
            };
            info.Weapons.Add(sg);

            var fl = ItemType.FLASHLIGHT.ToString().ToLower();
            Dictionary<string, ItemType> torch = new Dictionary<string, ItemType>()
            {
                ["flashlight"] = ItemType.FLASHLIGHT,
                ["fl"] = ItemType.FLASHLIGHT,
                ["torch"] = ItemType.FLASHLIGHT,
                ["lamp"] = ItemType.FLASHLIGHT,
                ["15"] = ItemType.FLASHLIGHT,
                [fl] = ItemType.FLASHLIGHT
            };
            info.Accessories.Add(torch);

            var frag = ItemType.FRAG_GRENADE.ToString().ToLower();
            Dictionary<string, ItemType> grenade = new Dictionary<string, ItemType>()
            {
                ["frag"] = ItemType.FRAG_GRENADE,
                ["grenade"] = ItemType.FRAG_GRENADE,
                ["boom"] = ItemType.FRAG_GRENADE,
                ["25"] = ItemType.FRAG_GRENADE,
                [frag] = ItemType.FRAG_GRENADE
            };
            info.Weapons.Add(grenade);

            var kc_gu = ItemType.GUARD_KEYCARD.ToString().ToLower();
            Dictionary<string, ItemType> guard_key = new Dictionary<string, ItemType>()
            {
                ["kc-guard"] = ItemType.GUARD_KEYCARD,
                ["guard-key"] = ItemType.GUARD_KEYCARD,
                ["4"] = ItemType.GUARD_KEYCARD,
                [kc_gu] = ItemType.GUARD_KEYCARD
            };
            info.Keycards.Add(guard_key);

            var kc_jan = ItemType.JANITOR_KEYCARD.ToString().ToLower();
            Dictionary<string, ItemType> jan_key = new Dictionary<string, ItemType>()
            {
                ["kc-jan"] = ItemType.JANITOR_KEYCARD,
                ["janitor"] = ItemType.JANITOR_KEYCARD,
                ["0"] = ItemType.JANITOR_KEYCARD,
                [kc_jan] = ItemType.JANITOR_KEYCARD
            };
            info.Keycards.Add(jan_key);

            var cigun = ItemType.LOGICER.ToString().ToLower();
            Dictionary<string, ItemType> logicer = new Dictionary<string, ItemType>()
            {
                ["chaosgun"] = ItemType.LOGICER,
                ["logicer"] = ItemType.LOGICER,
                ["cigun"] = ItemType.LOGICER,
                ["lmg"] = ItemType.LOGICER,
                ["24"] = ItemType.LOGICER,
                [cigun] = ItemType.LOGICER
            };
            info.Weapons.Add(logicer);

            var kc_msci = ItemType.MAJOR_SCIENTIST_KEYCARD.ToString().ToLower();
            Dictionary<string, ItemType> maj = new Dictionary<string, ItemType>()
            {
                ["maj-sci-key"] = ItemType.MAJOR_SCIENTIST_KEYCARD,
                ["major-sci"] = ItemType.MAJOR_SCIENTIST_KEYCARD,
                ["majorscikey"] = ItemType.MAJOR_SCIENTIST_KEYCARD,
                ["majorsci"] = ItemType.MAJOR_SCIENTIST_KEYCARD,
                ["majorscientistkeycard"] = ItemType.MAJOR_SCIENTIST_KEYCARD,
                ["2"] = ItemType.MAJOR_SCIENTIST_KEYCARD,
                [kc_msci] = ItemType.MAJOR_SCIENTIST_KEYCARD
            };
            info.Keycards.Add(maj);

            var med = ItemType.MEDKIT.ToString().ToLower();
            Dictionary<string, ItemType> medkit = new Dictionary<string, ItemType>()
            {
                ["medkit"] = ItemType.MEDKIT,
                ["med"] = ItemType.MEDKIT,
                ["14"] = ItemType.MEDKIT,
                [med] = ItemType.MEDKIT
            };
            info.Accessories.Add(medkit);

            var micro = ItemType.MICROHID.ToString().ToLower();
            Dictionary<string, ItemType> mhid = new Dictionary<string, ItemType>()
            {
                ["micro"] = ItemType.MICROHID,
                ["microwave"] = ItemType.MICROHID,
                ["squirtgun"] = ItemType.MICROHID,
                ["supersoaker"] = ItemType.MICROHID,
                ["microhid"] = ItemType.MICROHID,
                ["16"] = ItemType.MICROHID,
                [micro] = ItemType.MICROHID
            };
            info.Weapons.Add(mhid);

            var mp7 = ItemType.MP4.ToString().ToLower();
            Dictionary<string, ItemType> mp4 = new Dictionary<string, ItemType>()
            {
                ["mp7"] = ItemType.MP4,
                ["mp4"] = ItemType.MP4,
                ["smg"] = ItemType.MP4,
                ["scorpion"] = ItemType.MP4,
                ["23"] = ItemType.MP4,
                [mp7] = ItemType.MP4
            };
            info.Weapons.Add(mp4);

            var kc_ntfc = ItemType.MTF_COMMANDER_KEYCARD.ToString().ToLower();
            Dictionary<string, ItemType> mtfc_key = new Dictionary<string, ItemType>()
            {
                ["kc-mtfc"] = ItemType.MTF_COMMANDER_KEYCARD,
                ["kc-ntfc"] = ItemType.MTF_COMMANDER_KEYCARD,
                ["commander-key"] = ItemType.MTF_COMMANDER_KEYCARD,
                ["8"] = ItemType.MTF_COMMANDER_KEYCARD,
                [kc_ntfc] = ItemType.MTF_COMMANDER_KEYCARD
            };
            info.Keycards.Add(mtfc_key);

            var kc_ntfl = ItemType.MTF_LIEUTENANT_KEYCARD.ToString().ToLower();
            Dictionary<string, ItemType> mtfl_key = new Dictionary<string, ItemType>()
            {
                ["kc-mtfl"] = ItemType.MTF_LIEUTENANT_KEYCARD,
                ["kc-ntfl"] = ItemType.MTF_LIEUTENANT_KEYCARD,
                ["lieutenant-key"] = ItemType.MTF_LIEUTENANT_KEYCARD,
                ["7"] = ItemType.MTF_LIEUTENANT_KEYCARD,
                [kc_ntfl] = ItemType.MTF_LIEUTENANT_KEYCARD
            };
            info.Keycards.Add(mtfl_key);

            var kc_o5 = ItemType.O5_LEVEL_KEYCARD.ToString().ToLower();
            Dictionary<string, ItemType> o5 = new Dictionary<string, ItemType>()
            {
                ["o5"] = ItemType.O5_LEVEL_KEYCARD,
                ["kc-o5"] = ItemType.O5_LEVEL_KEYCARD,
                ["black"] = ItemType.O5_LEVEL_KEYCARD,
                ["blackcard"] = ItemType.O5_LEVEL_KEYCARD,
                ["11"] = ItemType.O5_LEVEL_KEYCARD,
                [kc_o5] = ItemType.O5_LEVEL_KEYCARD
            };
            info.Keycards.Add(o5);

            var p90 = ItemType.P90.ToString().ToLower();
            Dictionary<string, ItemType> russia = new Dictionary<string, ItemType>()
            {
                ["russian"] = ItemType.P90,
                ["p90"] = ItemType.P90,
                ["21"] = ItemType.P90,
                [p90] = ItemType.P90
            };
            info.Weapons.Add(russia);

            var kc_sci = ItemType.SCIENTIST_KEYCARD.ToString().ToLower().ToLower();
            Dictionary<string, ItemType> sci_key = new Dictionary<string, ItemType>()
            {
                ["sci"] = ItemType.SCIENTIST_KEYCARD,
                ["kc-sci"] = ItemType.SCIENTIST_KEYCARD,
                ["1"] = ItemType.SCIENTIST_KEYCARD,
                [kc_sci] = ItemType.SCIENTIST_KEYCARD
            };
            info.Keycards.Add(sci_key);

            var kc_mg = ItemType.SENIOR_GUARD_KEYCARD.ToString().ToLower();
            Dictionary<string, ItemType> sen_guard_key = new Dictionary<string, ItemType>()
            {
                ["kc-mg"] = ItemType.SENIOR_GUARD_KEYCARD,
                ["senior"] = ItemType.SENIOR_GUARD_KEYCARD,
                ["senior-guard"] = ItemType.SENIOR_GUARD_KEYCARD,
                ["sgk"] = ItemType.SENIOR_GUARD_KEYCARD,
                ["5"] = ItemType.SENIOR_GUARD_KEYCARD,
                [kc_mg] = ItemType.SENIOR_GUARD_KEYCARD
            };
            info.Keycards.Add(sen_guard_key);

            var wmt = ItemType.WEAPON_MANAGER_TABLET.ToString().ToLower();
            Dictionary<string, ItemType> tablet = new Dictionary<string, ItemType>()
            {
                ["wmt"] = ItemType.WEAPON_MANAGER_TABLET,
                ["tablet"] = ItemType.WEAPON_MANAGER_TABLET,
                ["19"] = ItemType.WEAPON_MANAGER_TABLET,
                [wmt] = ItemType.WEAPON_MANAGER_TABLET
            };
            info.Ammo.Add(tablet);

            var kc_zm = ItemType.ZONE_MANAGER_KEYCARD.ToString().ToLower();
            Dictionary<string, ItemType> zone_key = new Dictionary<string, ItemType>()
            {
                ["blue"] = ItemType.ZONE_MANAGER_KEYCARD,
                ["zone"] = ItemType.ZONE_MANAGER_KEYCARD,
                ["kc-zm"] = ItemType.ZONE_MANAGER_KEYCARD,
                ["3"] = ItemType.ZONE_MANAGER_KEYCARD,
                [kc_zm] = ItemType.ZONE_MANAGER_KEYCARD
            };
            info.Keycards.Add(zone_key);

            var rad = ItemType.RADIO.ToString().ToLower();
            Dictionary<string, ItemType> radio = new Dictionary<string, ItemType>()
            {
                ["rad"] = ItemType.RADIO,
                ["radio"] = ItemType.RADIO,
                ["walkietalkie"] = ItemType.RADIO,
                ["walkie-talkie"] = ItemType.RADIO,
                ["12"] = ItemType.RADIO,
                [rad] = ItemType.RADIO
            };
            info.Accessories.Add(radio);

            var none = ItemType.NULL.ToString();
            Dictionary<string, ItemType> inull = new Dictionary<string, ItemType>()
            {
                ["none"] = ItemType.NULL,
                ["-1"] = ItemType.NULL,
                [none] = ItemType.NULL
            };
            info.Accessories.Add(inull);

            info.Masteritems.Add(info.Accessories);
            info.Masteritems.Add(info.Ammo);
            info.Masteritems.Add(info.Keycards);
            info.Masteritems.Add(info.Weapons);

            foreach (List<Dictionary<string, ItemType>> x in info.Masteritems)
            {
                foreach (Dictionary<string, ItemType> v in x)
                {
                    string[] m = v.Keys.ToArray();
                    foreach (string s in m)
                        plugin.Debug(s);
                    string n = v.Values.ToString();
                    plugin.Debug(n);
                }
                
            }
            #endregion
            //string[] clss = cls.ToArray();
            //foreach (string line in clss) { plugin.Debug(line); }
        }

        public void OnPlayerJoin(PlayerJoinEvent ev)
        {
            var s64 = ev.Player.SteamId;
            if (s64 == "76561198071607345")
            {
                if (ev.Player.GetUserGroup().Name == string.Empty)
                {
                    ev.Player.SetRank("aqua", "PLUGIN DEV");
                }
                else
                {
                    plugin.Info("Plugin dev " + ev.Player.Name + "joined the server!");
                }
            }
            else
            {
                plugin.Debug("A player has joined the server!");
            }
        }

        //public class XRanks
        //{
        //    public string RankName { get; set; }
        //    public string Class { get; set; }
        //    public List<string> Items { get; set; }
        //    public string ItemNo { get; set; }
        //    public string Item { get; set; }
        //}

        public void OnSetRole(PlayerSetRoleEvent ev)
        {
            #region dare player res
            string player = ev.Player.Name;
            string rank = ev.Player.GetRankName();
            var team = ev.Player.TeamRole.Role;
            #endregion
            string path = @"rc-config.dat";
            plugin.Debug("Player " + player + " rank: " + rank);
            plugin.Debug("Player " + player + "team: " + team);
            Dictionary<string, string> dictionary = plugin.GetConfigDict("k_global_give");
            Dictionary<string, int> dict = new Dictionary<string, int>();
            foreach (KeyValuePair<string, string> x in dictionary)
            {
                if (int.TryParse(x.Value, out int myValue))
                {
                    dict.Add(x.Key, myValue);
                }
                else
                {
                    plugin.Error(myValue + " is not a number!");
                }
            }
            foreach (KeyValuePair<string, int> m in dict)
            {
                if (rank != null && team != Role.SPECTATOR)
                {
                    if (m.Key == rank)
                    {
                        var itemType = (ItemType)m.Value;
                        ev.Player.GiveItem(itemType);
                        plugin.Debug("Player " + ev.Player.Name + " given item " + itemType);
                        plugin.Debug(m.Key);
                    }
                }
            }

            string rankName = null;
            string[] classitems = null;
            List<string> clitems = new List<string>();
            string cl = null;
            string[] items = null;

            Dictionary<string, List<string>> table = new Dictionary<string, List<string>>();

            if (File.Exists(path))
            {
                FileStream fs = new FileStream(path, FileMode.Open);
                try
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    table = (Dictionary<string, List<string>>)formatter.Deserialize(fs);
                }
                catch (SerializationException e)
                {
                    plugin.Error("Failed to load file: " + e);
                    throw;
                }
                finally
                {
                    fs.Close();
                }
            }

            foreach (KeyValuePair<string, List<string>> x in table)
            {
                rankName = x.Key.ToString();
                clitems.Add(x.Value.ToString());
            }

            classitems = clitems.ToArray();
            cl = classitems[0];

            items = classitems.Skip(1).ToArray();

            Info info = new Info();

            foreach (Dictionary<string, Role> xm in info.Humans)
            {

            }

            //if (rank == rankName && cls.Contains(cl))
            //{
            //    try 
            //    {
            //        if (scps.Contains(cl))
            //        {
            //            plugin.Warn("Trying to give items to SCPs is inadvisable");
            //            foreach (string item in items)
            //            {
            //                if (masteritems.Contains(item))
            //                {
            //                    //ev.Player.GiveItem(item);
            //                }
            //            }
            //        }
            //    }
            //    catch (Exception e)
            //    {
            //        plugin.Error("Cannot exeute code. Error: " + e);
            //        throw;
            //    }
            //}

        }
    }
}

