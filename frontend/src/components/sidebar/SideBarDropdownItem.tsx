import { useState } from "react";
import { useNavigate } from "react-router-dom";

export default function SideBarDropdownItem(props: {icon: React.ReactNode, label: string, isShort: boolean, path: string, items: string[]}) {
  const navigate = useNavigate();
  const [open, setOpen] = useState(false);

  return (
    <div>
      <div
        className="flex items-center h-6 gap-3 text-gray-400 hover:text-white cursor-pointer px-4"
        onClick={() => setOpen(!open)}
      >
        {props.icon}
        {!props.isShort && 
            <>
                <span>{props.label}</span>
                <span className={`transition-transform duration-200 ${open ? 'rotate-180' : ''}`}>▾</span>
            </>
        }
      </div>

      {open && !props.isShort && (
        <div className="flex flex-col mt-2">
          {props.items.map((item) => (
            <div
              key={item}
              className="pl-12 py-1 text-gray-400 hover:text-white cursor-pointer"
              onClick={(e) => {
                e.stopPropagation();
                navigate(props.path);
              }}
            >
              {item}
            </div>
          ))}
        </div>
      )}
    </div>
  );
}