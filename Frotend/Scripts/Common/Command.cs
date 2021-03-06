﻿/*
 * message code define
 * if you are changing this file, mind to rebuild both Frontend and Backend later
 */

namespace Common
{
    public enum Command
    {
        NONE,
        SBEGIN = 0,
        S_PLAYER_ENTER,
        S_ITEM_SPAWN,
        S_SPAWN,
        S_PLAYER_RESPAWN,
        S_PLAYER_ACTION,
        S_ENTITY_DESTROY,
        S_PLAYER_MOVE,
		S_PLAYER_ATTRIBUTE,  //
		S_SPRITE_MOVE,
        S_JUMP,
        S_ATTACK,
        S_EQUIP_WEAPON,
        S_HIT,
        S_TAKE_ITEM,
        S_EXIT,
        S_SPRITE_DIE,
        S_PLAYER_DIE,
        S_TIP_INFO,
		S_ITEM_APPLY, //
		S_GET_FRIENDS,
		S_RECV_MESSAGE,
		S_SEND_MESSAGE,
		S_BUY_ITEMS,
		S_LEVEL_UP,
		S_GIVE_GIFT,
		S_TRADE_BUY,
		S_TRADE_SELL,

		SEND,

        CBEGIN,
        C_LOGIN,
        C_REGISTER,
        C_PLAYER_ENTER,
        C_PLAYER_ATTACK,
        C_PLAYER_JUMP,
        C_PLAYER_MOVE,
        C_PLAYER_TAKE,
        C_POSITION_REVISE,
        C_ENEMY_CLOSING,
        C_DAMAGE,
		C_ITEM_APPLY,     //
		C_GET_FRIENDS,
		C_RECV_MESSAGE,
		C_SEND_MESSAGE,
		C_BUY_ITEMS,
		C_ADD_COIN,
		C_GIVE_GIFT,
		C_TRADE_BUY,
		C_TRADE_SELL,
		C_TRADE_UNDO_SELL,
		C_RECV_GIFT,

		CEND,

        DEBUGGING, // THE FOLLOWING MESSEGES ARE FOR DEBUGGING
        C_FIND_PATH,
        S_FIND_PATH,

        CMD_END, // DO NOT GREATER THAN UINT_MAX !!!
    }
}
